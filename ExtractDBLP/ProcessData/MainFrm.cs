using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProcessData
{
    public partial class MainFrm : Form
    {
        object lock_author = new object();
        object lock_inproceeding = new object();
        object lock_conference = new object();
        Dictionary<int, AuthorDBLP> dicAuthors = new Dictionary<int, AuthorDBLP>(1300000);
        Dictionary<int, InproceedingsDBLP> dicInproceedings = new Dictionary<int, InproceedingsDBLP>(1300000);
        Dictionary<int, ConferenceDBLP> dicConferences = new Dictionary<int, ConferenceDBLP>(7000);
        Dictionary<string, ConferenceDBLP> dicStringConferences = new Dictionary<string, ConferenceDBLP>(7000);

        Dictionary<int, compactAuthorDBLP> diccompactAuthors = new Dictionary<int, compactAuthorDBLP>();
        Dictionary<int, compactInproceedingsDBLP> diccompactInproceedings = new Dictionary<int, compactInproceedingsDBLP>();
        List<FileInfo> fileArticles = new List<FileInfo>(10);
        List<FileInfo> fileAuthors = new List<FileInfo>(10);
        List<FileInfo> fileInproceedings = new List<FileInfo>(10);
        List<FileInfo> fileProceedings = new List<FileInfo>(10);
        List<FileInfo> filePhdthesis = new List<FileInfo>(10);
        List<FileInfo> fileConferences = new List<FileInfo>(10);
        public MainFrm()
        {
            InitializeComponent();
        }

        private void btnLoadData_Click(object sender, EventArgs e)
        {
            Thread tr = new Thread(new ThreadStart(LoadData));
            tr.Start();
        }
        private void LoadData()
        {
            this.Invoke((MethodInvoker)delegate
            {
                this.Cursor = Cursors.WaitCursor;
            });
            var sw = Stopwatch.StartNew();
            DirectoryInfo dInfo = new DirectoryInfo(Path.GetFullPath(txtFolder.Text));
            fileArticles = dInfo.GetFiles(txtArticlesPrefix.Text + "*", SearchOption.TopDirectoryOnly).ToList();
            fileInproceedings = dInfo.GetFiles(txtInproceedingsPrefix.Text + "*", SearchOption.TopDirectoryOnly).ToList();
            fileProceedings = dInfo.GetFiles(txtProceedingsPrefix.Text + "*", SearchOption.TopDirectoryOnly).ToList();
            filePhdthesis = dInfo.GetFiles(txtPhdThesisPrefix.Text + "*", SearchOption.TopDirectoryOnly).ToList();
            fileAuthors = dInfo.GetFiles(txtAuthorPrefix.Text + "*", SearchOption.TopDirectoryOnly).ToList();
            fileConferences = dInfo.GetFiles(txtConferencePrefix.Text + "*", SearchOption.TopDirectoryOnly).ToList();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            //ParseAuthors();
            ParseInproceedings(Convert.ToInt32(txtYearTo.Text));
            if (chkSaveConference.Checked)
            {
                SaveConferenceData();
            }
            if (chkLoadFromSave.Checked)
            {
                ParseConferences();
            }

            sw.Stop();
            this.Invoke((MethodInvoker)delegate
            {
                txtMsgLog.Text += (string.Format("Done Authors={0}, Inproceedings={1}, Conferences={2}\r\n", dicAuthors.Count, dicInproceedings.Count, dicConferences.Count));
                txtMsgLog.Text += string.Format("Total: {0}:{1}:{2}\r\n", sw.Elapsed.Hours, sw.Elapsed.Minutes, sw.Elapsed.Seconds);

                txtMsgLog.SelectionStart = txtMsgLog.Text.Length;
                txtMsgLog.ScrollToCaret();

                dgvAuthors.Invoke((MethodInvoker)delegate
                {
                    if (dicAuthors.Count < 500000)
                    {
                        BindingSource _bindingSource = new BindingSource();
                        dgvAuthors.DataSource = _bindingSource;
                        _bindingSource.DataSource = dicAuthors.Values;
                        dgvAuthors.Refresh();
                    }
                });
                dgvInproceedings.Invoke((MethodInvoker)delegate
                {
                    if (dicInproceedings.Count < 500000)
                    {
                        BindingSource _bindingSource = new BindingSource();
                        dgvInproceedings.DataSource = _bindingSource;
                        _bindingSource.DataSource = dicInproceedings.Values;
                        dgvInproceedings.Refresh();
                    }
                });
                dgvConferences.Invoke((MethodInvoker)delegate
                {
                    try
                    {
                        BindingSource _bindingSource = new BindingSource();
                        dgvConferences.DataSource = _bindingSource;
                        _bindingSource.DataSource = dicConferences.Values.ToList();
                        dgvConferences.Refresh();
                    }
                    catch { };
                });

                this.Cursor = Cursors.Default;
            });
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        private void ParseConferences()
        {
            int fileindex = 0;
            dicConferences.Clear();
            dicStringConferences.Clear();
            Parallel.ForEach(fileConferences, x =>
            {
                // Parse the file
                IEnumerable<string> lines = File.ReadLines(x.FullName);
                int curline = 0;
                Parallel.ForEach(lines, l =>
                {
                    ConferenceDBLP c = ConferenceDBLP.CreateFromLine(l, curline, fileindex);
                    if (c != null)
                    {
                        lock (lock_conference)
                        {
                            if (!dicStringConferences.ContainsKey(c.Key))
                            {
                                dicStringConferences.Add(c.Key, c);
                            }

                        }
                    }
                    curline++;
                });
                fileindex++;
            });
        }
        private void ParseAuthors()
        {
            dicAuthors.Clear();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            string prefex_saved = chkLoadFromSave.Checked ? "\\saved-" : "\\";
            int startValue = chkSetStartValue.Checked ? Convert.ToInt32(txtStartValue.Text) : -1;
            Parallel.ForEach(fileAuthors, x =>
            {
                int fileindex = x.Name.LastIndexOf('.');
                fileindex = Convert.ToInt32(x.Name.Substring(fileindex - 1, 1));
                IEnumerable<string> lines;

                lines = File.ReadLines(Path.GetFullPath(x.DirectoryName + prefex_saved + x.Name));

                int curline = 0;
                Parallel.ForEach(lines, l =>
                {
                    AuthorDBLP a = AuthorDBLP.CreateFromLine(l, curline, fileindex, startValue);
                    if (a != null)
                    {
                        int akey = 0;
                        lock (lock_author)
                        {
                            if (int.TryParse(a.Author_Keys, out akey) && !dicAuthors.ContainsKey(akey))
                            {

                                dicAuthors.Add(akey, a);
                            }
                        }
                    }
                    curline++;
                });

                this.Invoke((MethodInvoker)delegate
                {
                    txtMsgLog.Text += (string.Format("File index ={0} - {1} \r\n", fileindex, prefex_saved + x.Name));
                    txtMsgLog.SelectionStart = txtMsgLog.Text.Length;
                    txtMsgLog.ScrollToCaret();
                    if (txtMsgLog.Text.Length > 1000)
                    {
                        txtMsgLog.Text = "";
                    }
                });
            });

        }
        private void ParseCompactAuthors()
        {
            diccompactAuthors.Clear();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            string prefex_saved = chkLoadFromSave.Checked ? "\\saved-" : "\\";
            int startValue = chkSetStartValue.Checked ? Convert.ToInt32(txtStartValue.Text) : -1;
            Parallel.ForEach(fileAuthors, x =>
            {
                int fileindex = x.Name.LastIndexOf('.');
                fileindex = Convert.ToInt32(x.Name.Substring(fileindex - 1, 1));
                IEnumerable<string> lines;

                lines = File.ReadLines(Path.GetFullPath(x.DirectoryName + prefex_saved + x.Name));

                int curline = 0;
                Parallel.ForEach(lines, l =>
                {
                    compactAuthorDBLP a = compactAuthorDBLP.CreateFromLine(l, curline, fileindex, startValue);
                    if (a != null)
                    {

                        lock (lock_author)
                        {
                            if (!diccompactAuthors.ContainsKey(a.Key))
                            {

                                diccompactAuthors.Add(a.Key, a);
                            }
                        }
                    }
                    curline++;
                });

                this.Invoke((MethodInvoker)delegate
                {
                    txtMsgLog.Text += (string.Format("File index ={0} - {1} \r\n", fileindex, prefex_saved + x.Name));
                    txtMsgLog.SelectionStart = txtMsgLog.Text.Length;
                    txtMsgLog.ScrollToCaret();
                    if (txtMsgLog.Text.Length > 1000)
                    {
                        txtMsgLog.Text = "";
                    }
                });
            });

        }
        private void ParseInproceedings(int year)
        {

            int global_conference_id = 0;
            dicInproceedings.Clear();
            dicConferences.Clear();
            dicStringConferences.Clear();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            string prefex_saved = "\\";

            Parallel.ForEach(fileInproceedings, x =>
            {
                int fileindex = x.Name.LastIndexOf('.');
                fileindex = Convert.ToInt32(x.Name.Substring(fileindex - 1, 1));
                // Parse the file
                IEnumerable<string> lines;
                lines = File.ReadLines(Path.GetFullPath(x.DirectoryName + prefex_saved + x.Name));
                int curline = 0;
                Parallel.ForEach(lines, l =>
                {
                    InproceedingsDBLP inproceeding = InproceedingsDBLP.CreateFromLine(l, curline, fileindex, year);
                    if (inproceeding != null)
                    {
                        lock (lock_inproceeding)
                        {
                            dicInproceedings.Add(inproceeding.Id, inproceeding);
                        }
                        if (chkSaveConference.Checked)
                        {
                            lock (lock_conference)
                            {
                                string[] keys = inproceeding.Key.Split('/');
                                string conf_key = keys.Length > 1 ? keys[1] : keys[0];
                                if (dicStringConferences.ContainsKey(conf_key))
                                {
                                    ConferenceDBLP conference = dicStringConferences[conf_key];
                                    conference.InproceedingsID += "|" + inproceeding.Id;
                                    conference.CountInproceedings++;
                                }
                                else
                                {
                                    inproceeding.ConferenceID = global_conference_id;
                                    ConferenceDBLP conference = new ConferenceDBLP(conf_key, inproceeding.Conference, 1, inproceeding.Id.ToString(), global_conference_id);
                                    global_conference_id++;
                                    dicConferences.Add(conference.Id, conference);
                                    dicStringConferences.Add(conf_key, conference);
                                }
                            }
                        }
                    }
                    curline++;
                    if (curline % 100000 == 0)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            txtMsgLog.Text += (string.Format("Line Index= {0} - File index={1} - {2} \r\n", curline, fileindex, x.Name));
                            txtMsgLog.SelectionStart = txtMsgLog.Text.Length;
                            txtMsgLog.ScrollToCaret();
                            if (txtMsgLog.Text.Length > 1000)
                            {
                                txtMsgLog.Text = "";
                            }
                        });
                    }
                });

            });
        }
        private void ParseCompactInproceedings(int year)
        {           
            diccompactInproceedings.Clear();
            dicConferences.Clear();
            dicStringConferences.Clear();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            string prefex_saved = "\\";

            Parallel.ForEach(fileInproceedings, x =>
            {
                int fileindex = x.Name.LastIndexOf('.');
                fileindex = Convert.ToInt32(x.Name.Substring(fileindex - 1, 1));
                // Parse the file
                IEnumerable<string> lines;
                lines = File.ReadLines(Path.GetFullPath(x.DirectoryName + prefex_saved + x.Name));
                int curline = 0;
                Parallel.ForEach(lines, l =>
                {
                    compactInproceedingsDBLP inproceeding = compactInproceedingsDBLP.CreateFromLine(l, curline, fileindex, year);
                    if (inproceeding != null)
                    {
                        lock (lock_inproceeding)
                        {
                            diccompactInproceedings.Add(inproceeding.Key, inproceeding);
                        }
                    }
                    curline++;
                    if (curline % 100000 == 0)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            txtMsgLog.Text += (string.Format("Line Index= {0} - File index={1} - {2} \r\n", curline, fileindex, x.Name));
                            txtMsgLog.SelectionStart = txtMsgLog.Text.Length;
                            txtMsgLog.ScrollToCaret();
                            if (txtMsgLog.Text.Length > 1000)
                            {
                                txtMsgLog.Text = "";
                            }
                        });
                    }
                });

            });
        }
        private void btnBuildID_Click(object sender, EventArgs e)
        {
            Thread tr = new Thread(new ThreadStart(CreateInproceedingsID));
            tr.Start();
        }
        private void CreateInproceedingsID()
        {
            this.Invoke((MethodInvoker)delegate
            {
                this.Cursor = Cursors.WaitCursor;
            });
            var sw = Stopwatch.StartNew();
            int count = 0;
            //var dicIns = from d in dicInproceedings.AsParallel().Where(di => di.Value.Id < 100) select d;

            Parallel.ForEach(dicInproceedings, inproceeding =>
            {
                string[] authorsID = inproceeding.Value.AuthorsID.Split('|');
                string[] authorsName = inproceeding.Value.AUTHORNAMEs.Split('|');
                Parallel.For(0, authorsID.Length, i =>
                {
                    int akey = 0;
                    if (int.TryParse(authorsID[i], out akey))
                    {
                        lock (lock_author)
                        {
                            if (dicAuthors.ContainsKey(akey))
                            {
                                AuthorDBLP updateAuthor = dicAuthors[akey];
                                updateAuthor.UpdateInproceedings(inproceeding.Key, inproceeding.Value);
                            }
                            else
                            {
                                AuthorDBLP newAuthor = new AuthorDBLP()
                                {
                                    Id = dicAuthors.Count,
                                    Author_Keys = authorsID[i],
                                    Key = "person/hashcode/" + authorsID[i],
                                    Name = authorsName[i],
                                    InproceedingsID = "|" + inproceeding.Value.Id,
                                    CountInproceedings = 1,
                                    LineIndex = dicAuthors.Count,
                                    FileIndex = fileAuthors.Count,
                                    OldValue = Convert.ToInt32(txtStartValue.Text),
                                    CurrentValue = Convert.ToInt32(txtStartValue.Text)
                                };
                                dicAuthors.Add(akey, newAuthor);
                            }
                        }
                    }
                });

                if (count++ % 100000 == 0)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        txtMsgLog.Text += (string.Format("{0} Inproceeding={1} - {2} - Count Authors={3}\r\n", count, inproceeding.Value.Id, inproceeding.Value.Key, inproceeding.Value.CountAuthors));

                        txtMsgLog.SelectionStart = txtMsgLog.Text.Length;
                        txtMsgLog.ScrollToCaret();
                        if (txtMsgLog.Text.Length > 1000)
                        {
                            txtMsgLog.Text = "";
                        }

                    });
                }
            });
            if (chkSaveAuthor.Checked)
            {
                dicInproceedings.Clear();
                GC.Collect();
                GC.WaitForPendingFinalizers();
                SaveAuthorsData();
                ParseInproceedings(Convert.ToInt32(txtYearTo.Text));
            }
            sw.Stop();
            this.Invoke((MethodInvoker)delegate
            {
                dgvAuthors.Invoke((MethodInvoker)delegate
                {
                    dgvAuthors.DataSource = dicAuthors.Values.ToList();
                    dgvAuthors.Refresh();

                });
                dgvInproceedings.Invoke((MethodInvoker)delegate
                {
                    dgvInproceedings.DataSource = dicInproceedings.Values.ToList();
                    dgvInproceedings.Refresh();
                });
                dgvConferences.Invoke((MethodInvoker)delegate
                {
                    dgvConferences.DataSource = dicConferences.Values.ToList();
                    dgvConferences.Refresh();
                });
                txtMsgLog.Text += (string.Format("Done Authors={0}, Inproceedings={1}, Conferences={2}\r\n", dicAuthors.Count, dicInproceedings.Count, dicConferences.Count));
                txtMsgLog.Text += string.Format("Total time: {0}:{1}:{2}\r\n", sw.Elapsed.Hours, sw.Elapsed.Minutes, sw.Elapsed.Seconds);
                txtMsgLog.SelectionStart = txtMsgLog.Text.Length;
                txtMsgLog.ScrollToCaret();
                this.Cursor = Cursors.Default;
            });

        }
        private void SaveConferenceData()
        {
            StringBuilder sb = new StringBuilder();
            foreach (ConferenceDBLP x in dicConferences.Values)
            {
                sb.AppendLine(x.ToString());
            }
            File.WriteAllText(Path.GetFullPath(txtFolder.Text + "\\" + txtConferencePrefix.Text + "0.csv"), sb.ToString());
        }
        private void SaveAuthorsData()
        {
            Parallel.For(1, fileAuthors.Count + 1, i =>
            {
                StringBuilder sb = new StringBuilder();

                var dicA = dicAuthors.Where(d => d.Value.FileIndex == i).OrderBy(d => d.Value.LineIndex);
                dicA.ToList().ForEach(dA =>
                {
                    sb.AppendLine(dA.Value.ToString());
                });

                File.WriteAllText(Path.GetFullPath(txtFolder.Text + "\\saved-" +
                    txtAuthorPrefix.Text + i.ToString() + ".csv"), sb.ToString());
            });
        }
        private void SaveInproceedingsData()
        {
            Parallel.For(1, fileInproceedings.Count + 1, i =>
            {
                StringBuilder sb = new StringBuilder();

                var dicI = dicInproceedings.Where(d => d.Value.FileIndex == i).OrderBy(d => d.Value.LineIndex);
                dicI.ToList().ForEach(dI =>
                {
                    sb.AppendLine(dI.Value.ToString());
                });

                File.WriteAllText(Path.GetFullPath(txtFolder.Text + "\\saved-" +
                    txtInproceedingsPrefix.Text + i.ToString() + ".csv"), sb.ToString());
            });
        }
        object lock_acal = new object();
        object lock_ical = new object();
        object lock_ccal = new object();
        private void btnStart_Click(object sender, EventArgs e)
        {
            txtMsgLog.Text = "Start All";
            this.Cursor = Cursors.WaitCursor;
            double sumAs1, sumIs1, sumCs, sumIs2, sumAs2, min, ratio, max;
            max = 1000;
            var sw = Stopwatch.StartNew();
            while (max > Convert.ToDouble(txtMinDelta.Text))
            {
                sw.Start();
                sumAs1 = dicAuthors.AsParallel().Sum(x => x.Value.CurrentValue);
                Parallel.ForEach(dicInproceedings, d =>
                {
                    d.Value.SetValueFromAuthors(dicAuthors);
                });
                sumIs1 = dicInproceedings.AsParallel().Sum(x => x.Value.CurrentValue);
                dicConferences.AsParallel().ForAll(x => x.Value.SetValueFromInproceedings(dicInproceedings));
                sumCs = dicConferences.AsParallel().Sum(x => x.Value.CurrentValue);
                dicInproceedings.AsParallel().ForAll(x => x.Value.SetValueFromConferences(dicConferences));
                sumIs2 = dicInproceedings.AsParallel().Sum(x => x.Value.CurrentValue);
                dicAuthors.AsParallel().ForAll(x => x.Value.SetValueFromInproceedings(dicInproceedings));
                sumAs2 = dicAuthors.AsParallel().Sum(x => x.Value.CurrentValue);
                ratio = sumAs2 / sumAs1;
                min = dicAuthors.AsParallel().Min(x => Math.Abs(x.Value.CurrentValue - x.Value.OldValue));
                max = dicAuthors.AsParallel().Max(x => Math.Abs(x.Value.CurrentValue - x.Value.OldValue));
                StringBuilder sb = new StringBuilder();
                for (int i = 1; i < fileAuthors.Count + 1; i++)
                {
                    foreach (KeyValuePair<int, AuthorDBLP> d in dicAuthors)
                    {
                        if (d.Value.FileIndex == i)
                        {
                            sb.AppendLine(d.Value.ToString());
                        }
                    }

                    File.WriteAllText(Path.GetFullPath(txtFolder.Text + "\\cal-" + ((int)max).ToString() +
                        txtAuthorPrefix.Text + i.ToString() + ".csv"), sb.ToString());
                    sb.Clear();
                }
                for (int i = 1; i < fileInproceedings.Count + 1; i++)
                {
                    foreach (KeyValuePair<int, InproceedingsDBLP> d in dicInproceedings)
                    {
                        if (d.Value.FileIndex == i)
                        {
                            sb.AppendLine(d.Value.ToString());
                        }
                    }

                    File.WriteAllText(Path.GetFullPath(txtFolder.Text + "\\cal-" + ((int)max).ToString() +
                        txtInproceedingsPrefix.Text + i.ToString() + ".csv"), sb.ToString());
                    sb.Clear();
                }
                for (int i = 1; i < fileConferences.Count + 1; i++)
                {
                    foreach (KeyValuePair<int, ConferenceDBLP> d in dicConferences)
                    {
                        sb.AppendLine(d.Value.ToString());
                    }

                    File.WriteAllText(Path.GetFullPath(txtFolder.Text + "\\cal-" + ((int)max).ToString() +
                        txtConferencePrefix.Text + i.ToString() + ".csv"), sb.ToString());
                    sb.Clear();
                }
                sw.Stop();
                txtMsgLog.Text += string.Format("Total: {0}:{1}:{2} Max={3}\r\n", sw.Elapsed.Hours, sw.Elapsed.Minutes, sw.Elapsed.Seconds, max);
                GC.Collect();
                GC.WaitForPendingFinalizers();
                txtMsgLog.SelectionStart = txtMsgLog.Text.Length;
                txtMsgLog.ScrollToCaret();
            }
            MessageBox.Show("Done");
            this.Cursor = Cursors.Default;
        }
        private void btnSaveData_Click(object sender, EventArgs e)
        {

        }

        private void btnStartByYear_Click(object sender, EventArgs e)
        {
            Thread tr = new Thread(new ThreadStart(ProcessingByYear));
            tr.Start();
        }

        private void ProcessingByYear()
        {

            Dictionary<int, InproceedingsDBLP> inproCals = dicInproceedings.Where(
                x =>
                {
                    int year;
                    if (int.TryParse(x.Value.Year, out year))
                        return Convert.ToInt32(x.Value.Year) <= Convert.ToInt32(txtYearTo.Text) &&
                            Convert.ToInt32(x.Value.Year) >= Convert.ToInt32(txtYearFrom.Text);
                    else
                        return false;
                }).ToDictionary(x => x.Key, x => x.Value);
            dicInproceedings.Clear();

            Dictionary<int, AuthorDBLP> authorCals = new Dictionary<int, AuthorDBLP>();
            Dictionary<string, ConferenceDBLP> confCals = new Dictionary<string, ConferenceDBLP>();

            Parallel.ForEach(inproCals, inproceeding =>
            {
                string[] AuthorsIDs = inproceeding.Value.AuthorsID.Split('|');
                Parallel.For(0, AuthorsIDs.Length, ai =>
                {
                    int akey = 0;
                    if (int.TryParse(AuthorsIDs[ai], out akey))
                    {
                        AuthorDBLP authorDblp = dicAuthors[akey];
                        lock (lock_acal)
                        {
                            if (!authorCals.Keys.Contains(akey))
                            {
                                authorCals.Add(akey, dicAuthors[akey]);
                                authorCals[akey].CountInproceedings = 1;
                                authorCals[akey].InproceedingsID = inproceeding.Value.Id.ToString();
                                authorCals[akey].CurrentValue = 1000;
                                authorCals[akey].OldValue = 1000;
                            }
                            else
                            {
                                authorCals[akey].CountInproceedings++;
                                authorCals[akey].InproceedingsID += "|" + inproceeding.Value.Id.ToString();
                            }
                        }
                    }
                });
                lock (lock_ccal)
                {
                    string[] keys = inproceeding.Value.Key.Split('/');
                    string conf_key = keys.Length > 1 ? keys[1] : keys[0];
                    if (!confCals.Keys.Contains(conf_key))
                    {
                        confCals.Add(conf_key, dicStringConferences[conf_key]);
                        confCals[conf_key].CountInproceedings = 1;
                        confCals[conf_key].InproceedingsID = inproceeding.Value.Id.ToString();
                    }
                    else
                    {
                        confCals[conf_key].CountInproceedings++;
                        confCals[conf_key].InproceedingsID += "|" + inproceeding.Value.Id.ToString();
                    }
                }
            });
            dicAuthors.Clear();
            dicStringConferences.Clear();
            dicConferences.Clear();
            this.Invoke((MethodInvoker)delegate
            {
                txtMsgLog.Text = "Begin...";
                Cursor = Cursors.WaitCursor;
            });
            double sumAs1 = 0, sumIs1 = 0, sumCs = 0, sumIs2 = 0, sumAs2 = 0, min, ratio, max, max_old = 0, sum0;
            max = 1000;
            var sw = Stopwatch.StartNew();
            ratio = 1;
            sum0 = authorCals.AsParallel().Sum(x => x.Value.CurrentValue);

            while (max > Convert.ToDouble(txtMinDelta.Text))
            {
                sw.Start();


                sumAs1 = authorCals.AsParallel().Sum(x => x.Value.CurrentValue);
                inproCals.AsParallel().ForAll(x => x.Value.SetValueFromAuthors(authorCals));
                sumIs1 = inproCals.AsParallel().Sum(x => x.Value.CurrentValue);
                confCals.AsParallel().ForAll(x => x.Value.SetValueFromInproceedings(inproCals));
                sumCs = confCals.AsParallel().Sum(x => x.Value.CurrentValue);
                inproCals.AsParallel().ForAll(x => x.Value.SetValueFromConferences(confCals));
                sumIs2 = inproCals.AsParallel().Sum(x => x.Value.CurrentValue);
                authorCals.AsParallel().ForAll(x => x.Value.SetValueFromInproceedings(inproCals));
                sumAs2 = authorCals.AsParallel().Sum(x => x.Value.CurrentValue);
                ratio = sum0 / sumAs2;
                min = authorCals.AsParallel().Min(x => Math.Abs(x.Value.CurrentValue - x.Value.OldValue));
                max = authorCals.AsParallel().Max(x => Math.Abs(x.Value.CurrentValue - x.Value.OldValue));
                if ((ratio - 1) > 0.009)
                {
                    authorCals.AsParallel().ForAll(x => x.Value.OldValue = x.Value.CurrentValue);
                    authorCals.AsParallel().ForAll(x => x.Value.CurrentValue = x.Value.CurrentValue * ratio);
                    //          authorCals.AsParallel().ForAll(x => x.Value.CurrentValue = x.Value.CurrentValue * ratio * 0.8 + 200);   
                }
                if (Math.Abs(max_old - max) > 50)
                {
                    max_old = max;
                    WriteResultToFile(max, txtResultPrefix.Text, authorCals, inproCals, confCals);
                }
                sw.Stop();
                this.Invoke((MethodInvoker)delegate
                {
                    txtMsgLog.Text += string.Format("Total: {0}:{1}:{2} Max={3} Ratio={12}(Sum AS1:{4}-SumIS1:{5}-sumCs:{6}-sumIs2:{7}- sumAs2{8} (A:{9}-P:{10}-C{11})\r\n",
                        sw.Elapsed.Hours, sw.Elapsed.Minutes, sw.Elapsed.Seconds, max, sumAs1, sumIs1, sumCs, sumIs2, sumAs2, authorCals.Count, inproCals.Count, confCals.Count, ratio);
                    txtMsgLog.SelectionStart = txtMsgLog.Text.Length;
                    txtMsgLog.ScrollToCaret();
                });
            }
            WriteResultToFile(max, txtResultPrefix.Text, authorCals, inproCals, confCals);
            File.WriteAllText(Path.GetFullPath(txtFolder.Text + "\\" + txtResultPrefix.Text + "=" + txtYearTo.Text + "-" + ((int)max).ToString() +
                    "-log.csv"), txtMsgLog.Text);
            this.Invoke((MethodInvoker)delegate
            {
                txtMsgLog.Text += "Done!!!";
                this.Cursor = Cursors.Default;
            });
        }

        private void WriteResultToFile(double max, string prefix, Dictionary<int, AuthorDBLP> authorCals, Dictionary<int, InproceedingsDBLP> inproCals, Dictionary<string, ConferenceDBLP> confCals)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 1; i < fileAuthors.Count + 1; i++)
            {
                foreach (KeyValuePair<int, AuthorDBLP> d in authorCals)
                {
                    if (d.Value.FileIndex == i)
                    {
                        sb.AppendLine(d.Value.ToString());
                    }
                }

                File.WriteAllText(Path.GetFullPath(txtFolder.Text + "\\" + prefix + ((int)max).ToString() +
                    txtAuthorPrefix.Text + i.ToString() + ".csv"), sb.ToString());
                sb.Clear();
            }
            for (int i = 1; i < fileInproceedings.Count + 1; i++)
            {
                foreach (KeyValuePair<int, InproceedingsDBLP> d in inproCals)
                {
                    if (d.Value.FileIndex == i)
                    {
                        sb.AppendLine(d.Value.ToString());
                    }
                }

                File.WriteAllText(Path.GetFullPath(txtFolder.Text + "\\" + prefix + ((int)max).ToString() +
                    txtInproceedingsPrefix.Text + i.ToString() + ".csv"), sb.ToString());
                sb.Clear();
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            for (int i = 1; i < fileConferences.Count + 1; i++)
            {
                foreach (KeyValuePair<string, ConferenceDBLP> d in confCals)
                {
                    sb.AppendLine(d.Value.ToString());
                }

                File.WriteAllText(Path.GetFullPath(txtFolder.Text + "\\" + prefix + ((int)max).ToString() +
                    txtConferencePrefix.Text + i.ToString() + ".csv"), sb.ToString());
                sb.Clear();
            }
        }

        private void btnCompactStart_Click(object sender, EventArgs e)
        {
            Thread tr = new Thread(new ThreadStart(ProcessingCompactByYear));
            tr.Start();
        }

        private void ProcessingCompactByYear()
        {
            this.Invoke((MethodInvoker)delegate
            {
                this.Cursor = Cursors.WaitCursor;
            });
            var sw = Stopwatch.StartNew();
            DirectoryInfo dInfo = new DirectoryInfo(Path.GetFullPath(txtFolder.Text));
            fileArticles = dInfo.GetFiles(txtArticlesPrefix.Text + "*", SearchOption.TopDirectoryOnly).ToList();
            fileInproceedings = dInfo.GetFiles(txtInproceedingsPrefix.Text + "*", SearchOption.TopDirectoryOnly).ToList();
            fileProceedings = dInfo.GetFiles(txtProceedingsPrefix.Text + "*", SearchOption.TopDirectoryOnly).ToList();
            filePhdthesis = dInfo.GetFiles(txtPhdThesisPrefix.Text + "*", SearchOption.TopDirectoryOnly).ToList();
            fileAuthors = dInfo.GetFiles(txtAuthorPrefix.Text + "*", SearchOption.TopDirectoryOnly).ToList();
            fileConferences = dInfo.GetFiles(txtConferencePrefix.Text + "*", SearchOption.TopDirectoryOnly).ToList();
            GC.Collect();
            GC.WaitForPendingFinalizers();

            ParseCompactAuthors();
            ParseCompactInproceedings(Convert.ToInt32(txtYearTo.Text));
            ParseConferences();
            Dictionary<int, compactInproceedingsDBLP> inproCals = diccompactInproceedings.Where(
                x => x.Value.Year <= Convert.ToInt32(txtYearTo.Text)).ToDictionary(x => x.Key, x => x.Value);
            diccompactInproceedings.Clear();

            Dictionary<int, compactAuthorDBLP> authorCals = new Dictionary<int, compactAuthorDBLP>();
            Dictionary<string, ConferenceDBLP> confCals = new Dictionary<string, ConferenceDBLP>();

            Parallel.ForEach(inproCals, inproceeding =>
            {
                Parallel.ForEach(inproceeding.Value.Authors, ai =>
                {

                    compactAuthorDBLP authorDblp = diccompactAuthors[ai];
                    lock (lock_acal)
                    {
                        if (!authorCals.Keys.Contains(ai))
                        {
                            authorCals.Add(ai, diccompactAuthors[ai]);
                            authorCals[ai].Count = 1;
                            authorCals[ai].Papers.Clear();
                            authorCals[ai].Papers.Add(inproceeding.Key);
                            authorCals[ai].CurrentValue = 1000;
                            authorCals[ai].OldValue = 1000;
                        }
                        else
                        {
                            authorCals[ai].Count++;
                            authorCals[ai].Papers.Add(inproceeding.Key);
                        }
                    }
                });
                lock (lock_ccal)
                {
                    if (!confCals.Keys.Contains(inproceeding.Value.Conference))
                    {
                        confCals.Add(inproceeding.Value.Conference, dicStringConferences[inproceeding.Value.Conference]);
                        confCals[inproceeding.Value.Conference].CountInproceedings = 1;
                        confCals[inproceeding.Value.Conference].InproceedingsID = inproceeding.Value.Key.ToString();
                    }
                    else
                    {
                        confCals[inproceeding.Value.Conference].CountInproceedings++;
                        confCals[inproceeding.Value.Conference].InproceedingsID += "|" + inproceeding.Value.Key.ToString();
                    }
                }
            });
            dicAuthors.Clear();
            dicStringConferences.Clear();
            dicConferences.Clear();
            this.Invoke((MethodInvoker)delegate
            {
                txtMsgLog.Text = "Begin...";
                Cursor = Cursors.WaitCursor;
            });
            double sumAs1 = 0, sumIs1 = 0, sumCs = 0, sumIs2 = 0, sumAs2 = 0, min, ratio, max, max_old = 0, sum0;
            max = 1000;

            ratio = 1;
            sum0 = authorCals.AsParallel().Sum(x => x.Value.CurrentValue);

            while (max > Convert.ToDouble(txtMinDelta.Text))
            {
                sw.Start();


                sumAs1 = authorCals.AsParallel().Sum(x => x.Value.CurrentValue);
                inproCals.AsParallel().ForAll(x => x.Value.SetValueFromAuthors(authorCals));
                sumIs1 = inproCals.AsParallel().Sum(x => x.Value.CurrentValue);
                confCals.AsParallel().ForAll(x => x.Value.SetValueFromInproceedings(inproCals));
                sumCs = confCals.AsParallel().Sum(x => x.Value.CurrentValue);
                inproCals.AsParallel().ForAll(x => x.Value.SetValueFromConferences(confCals));
                sumIs2 = inproCals.AsParallel().Sum(x => x.Value.CurrentValue);
                authorCals.AsParallel().ForAll(x => x.Value.SetValueFromInproceedings(inproCals));
                sumAs2 = authorCals.AsParallel().Sum(x => x.Value.CurrentValue);
                ratio = sum0 / sumAs2;
                min = authorCals.AsParallel().Min(x => Math.Abs(x.Value.CurrentValue - x.Value.OldValue));
                max = authorCals.AsParallel().Max(x => Math.Abs(x.Value.CurrentValue - x.Value.OldValue));
                if (Math.Abs(ratio - 1) > 0.009)
                {
                    authorCals.AsParallel().ForAll(x => x.Value.OldValue = x.Value.CurrentValue);
                    //authorCals.AsParallel().ForAll(x => x.Value.CurrentValue = x.Value.CurrentValue * ratio);
                    authorCals.AsParallel().ForAll(x => x.Value.CurrentValue = x.Value.CurrentValue * ratio * 0.8 + 200);   
                }
                if (Math.Abs(max_old - max) > 50)
                {
                    max_old = max;
                    WriteResultToFile(max, txtResultPrefix.Text, authorCals, inproCals, confCals);
                }
                sw.Stop();
                if (this.IsHandleCreated)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        txtMsgLog.Text += string.Format("Total: {0}:{1}:{2} Max={3} Ratio={12}(Sum AS1:{4}-SumIS1:{5}-sumCs:{6}-sumIs2:{7}- sumAs2{8} (A:{9}-P:{10}-C{11})\r\n",
                            sw.Elapsed.Hours, sw.Elapsed.Minutes, sw.Elapsed.Seconds, max, sumAs1, sumIs1, sumCs, sumIs2, sumAs2, authorCals.Count, inproCals.Count, confCals.Count, ratio);
                        txtMsgLog.SelectionStart = txtMsgLog.Text.Length;
                        txtMsgLog.ScrollToCaret();
                    });
                }
                else
                {
                    Thread.CurrentThread.Abort();
                }
            }
            WriteResultToFile(max, txtResultPrefix.Text, authorCals, inproCals, confCals);
            File.WriteAllText(Path.GetFullPath(txtFolder.Text + "\\" + txtResultPrefix.Text + "=" + txtYearTo.Text + "-" + ((int)max).ToString() +
                    "-log.csv"), txtMsgLog.Text);
            this.Invoke((MethodInvoker)delegate
            {
                txtMsgLog.Text += "Done!!!";
                this.Cursor = Cursors.Default;
            });
        }

        private void WriteResultToFile(double max, string prefix, Dictionary<int, compactAuthorDBLP> authorCals, Dictionary<int, compactInproceedingsDBLP> inproCals, Dictionary<string, ConferenceDBLP> confCals)
        {
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<int, compactAuthorDBLP> d in authorCals)
            {
                sb.AppendLine(d.Value.ToString());
            }

            File.WriteAllText(Path.GetFullPath(txtFolder.Text + "\\" + prefix + ((int)max).ToString() +
                txtAuthorPrefix.Text + ".csv"), sb.ToString());
            sb.Clear();


            for (int i = 1; i < fileInproceedings.Count + 1; i++)
            {
                foreach (KeyValuePair<int, compactInproceedingsDBLP> d in inproCals)
                {
                    if (d.Value.ifile == i)
                    {
                        sb.AppendLine(d.Value.ToString());
                    }
                }

                File.WriteAllText(Path.GetFullPath(txtFolder.Text + "\\" + prefix + ((int)max).ToString() +
                    txtInproceedingsPrefix.Text + i.ToString() + ".csv"), sb.ToString());
                sb.Clear();
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }


            foreach (KeyValuePair<string, ConferenceDBLP> d in confCals)
            {
                sb.AppendLine(d.Value.ToString());
            }

            File.WriteAllText(Path.GetFullPath(txtFolder.Text + "\\" + prefix + ((int)max).ToString() +
                txtConferencePrefix.Text + ".csv"), sb.ToString());
            sb.Clear();

        }
    }
}

