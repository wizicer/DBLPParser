using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using HtmlAgilityPack;

namespace PreProcessDBLP
{
    public partial class MainFrm : Form
    {
        object lock_author = new object();
        object lock_inproceeding = new object();
        object lock_conference = new object();
        Dictionary<int, DblpAuthors> dicAuthors = new Dictionary<int, DblpAuthors>(1300000);
        Dictionary<int, int> dicAuthorsAlias = new Dictionary<int, int>(1300000);
        Dictionary<int, DBLPInproceedings> dicInproceedings = new Dictionary<int, DBLPInproceedings>(1300000);
        Dictionary<int, DBLPConferences> dicConferences = new Dictionary<int, DBLPConferences>(7000);
        Dictionary<int, string> dicNotConferences = new Dictionary<int, string>();
        int currentTestConference;

        List<FileInfo> fileArticles = new List<FileInfo>();
        List<FileInfo> fileAuthors = new List<FileInfo>();
        List<FileInfo> fileInproceedings = new List<FileInfo>();
        List<FileInfo> fileProceedings = new List<FileInfo>();
        List<FileInfo> filePhdthesis = new List<FileInfo>();
        List<FileInfo> fileConferences = new List<FileInfo>();
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
            if (this.IsHandleCreated)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    this.Cursor = Cursors.WaitCursor;
                });
            }
            var sw = Stopwatch.StartNew();

            ParseAuthors();


            if (chkSaveArticles.Checked)
            {
                ParseArticles();
            }

            ParseInproceedings();

            if (chkSaveAuthors.Checked)
            {
                SaveAuthors();
            }
            if (chkSaveConference.Checked)
            {
                SaveConferences();
            }
            if (chkSaveInproceedings.Checked)
            {
                SaveInproceedingsData();
            }
            sw.Stop();
            if (this.IsHandleCreated)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    txtMsgLog.Text += (string.Format("Done Authors={0}, Inproceedings={1}, Conferences={2}\r\n", dicAuthors.Count, dicInproceedings.Count, dicConferences.Count));
                    txtMsgLog.Text += string.Format("Total: {0}:{1}:{2}\r\n", sw.Elapsed.Hours, sw.Elapsed.Minutes, sw.Elapsed.Seconds);

                    txtMsgLog.SelectionStart = txtMsgLog.Text.Length;
                    txtMsgLog.ScrollToCaret();
                    dgvAuthors.DataSource = dicAuthors.Take(1000).ToList();
                    dgvInproceedings.DataSource = dicInproceedings.Take(1000).ToList();
                    dgvConferences.DataSource = dicConferences.ToList();
                    this.Cursor = Cursors.Default;
                });
            }

        }

        private void ParseArticles()
        {
            int year = chkLoadBeforeYear.Checked ? Convert.ToInt32(txtBeforeYear.Text) : 2020;
            int startValue = chkSetStartValue.Checked ? Convert.ToInt32(txtStartValue.Text) : -1;
            Parallel.ForEach(fileArticles, x =>
            {
                int fileindex = x.Name.LastIndexOf('.');
                fileindex = Convert.ToInt32(x.Name.Substring(fileindex - 1, 1));
                IEnumerable<string> lines;

                lines = File.ReadLines(Path.GetFullPath(x.FullName));

                int curline = 1;
                //Parallel.ForEach(lines, l =>
                foreach (string l in lines)
                {
                    DBLPInproceedings inproceeding = DBLPInproceedings.CreateInproceedingsFromLine(1, l, curline, fileindex, year);
                    if (inproceeding != null)
                    {
                        lock (lock_inproceeding)
                        {

                            if (!dicInproceedings.ContainsKey(inproceeding.Key))
                            {
                                dicInproceedings.Add(inproceeding.Key, inproceeding);
                            }
                        }
                        lock (lock_author)
                        {
                            Parallel.ForEach(inproceeding.Authors, aut =>
                            {
                                if (dicAuthors.ContainsKey(aut))
                                {
                                    DblpAuthors author = dicAuthors[aut];
                                    if (!author.Inproceedings.Contains(inproceeding.Key))
                                    {
                                        author.Inproceedings.Add(inproceeding.Key);
                                        author.Count = author.Inproceedings.Count;
                                    }
                                }
                                else
                                {
                                    DblpAuthors author = new DblpAuthors(aut.ToString(), inproceeding.StringKey, inproceeding.Key.ToString(), null, 0, 0, fileindex, curline);
                                    dicAuthors.Add(aut, author);
                                }
                            });
                        }
                        lock (lock_conference)
                        {
                            string[] keys = inproceeding.StringKey.Split('/');
                            string conf_key = (keys.Length > 1 ? keys[1] : keys[0]) + "_" + inproceeding.type.ToString();
                            if (dicConferences.ContainsKey(conf_key.GetHashCode()))
                            {
                                DBLPConferences conference = dicConferences[conf_key.GetHashCode()];
                                if (!conference.Years.Contains(inproceeding.Year))
                                {
                                    conference.Years.Add(inproceeding.Year);
                                }
                                if (!conference.InproceedingsByYear.ContainsKey(inproceeding.Year))
                                {
                                    conference.InproceedingsByYear[inproceeding.Year] = new HashSet<int>();
                                    conference.InproceedingsByYear[inproceeding.Year].Add(inproceeding.Key);
                                }
                                else
                                {
                                    if (!conference.InproceedingsByYear[inproceeding.Year].Contains(inproceeding.Key))
                                    {
                                        conference.InproceedingsByYear[inproceeding.Year].Add(inproceeding.Key);
                                    }
                                }
                                conference.CountAll = conference.InproceedingsByYear.Sum(inpr => inpr.Value.Count);
                                inproceeding.Conference = conf_key.GetHashCode();
                            }
                            else
                            {
                                inproceeding.Conference = conf_key.GetHashCode();
                                DBLPConferences conference = new DBLPConferences(conf_key.GetHashCode(), conf_key,
                                    inproceeding.Year, inproceeding.Key, 1, startValue, startValue);
                                dicConferences.Add(conference.Key, conference);
                            }
                        }
                    }
                    if (curline % 100000 == 0)
                    {
                        if (this.IsHandleCreated)
                        {
                            this.Invoke((MethodInvoker)delegate
                            {
                                txtMsgLog.Text += (string.Format("File index ={0} Line index={1} - {2} \r\n", fileindex, curline, x.Name));
                                txtMsgLog.SelectionStart = txtMsgLog.Text.Length;
                                txtMsgLog.ScrollToCaret();
                                if (txtMsgLog.Text.Length > 1000)
                                {
                                    txtMsgLog.Text = "";
                                }
                            });
                        }
                    }
                    curline++;
                }
                //});


            });

        }


        private void ParseInproceedings()
        {
            int year = chkLoadBeforeYear.Checked ? Convert.ToInt32(txtBeforeYear.Text) : 2020;
            int startValue = chkSetStartValue.Checked ? Convert.ToInt32(txtStartValue.Text) : -1;
            Parallel.ForEach(fileInproceedings, x =>
            {
                int fileindex = x.Name.LastIndexOf('.');
                fileindex = Convert.ToInt32(x.Name.Substring(fileindex - 1, 1));
                IEnumerable<string> lines;

                lines = File.ReadLines(Path.GetFullPath(x.FullName));

                int curline = 1;
                //Parallel.ForEach(lines, l =>
                foreach (string l in lines)
                {
                    DBLPInproceedings inproceeding = DBLPInproceedings.CreateInproceedingsFromLine(0, l, curline, fileindex, year);
                    if (inproceeding != null)
                    {
                        lock (lock_inproceeding)
                        {

                            if (!dicInproceedings.ContainsKey(inproceeding.Key))
                            {
                                dicInproceedings.Add(inproceeding.Key, inproceeding);
                            }
                        }
                        lock (lock_author)
                        {
                            Parallel.ForEach(inproceeding.Authors, aut =>
                            {
                                if (dicAuthors.ContainsKey(aut))
                                {
                                    DblpAuthors author = dicAuthors[aut];
                                    if (!author.Inproceedings.Contains(inproceeding.Key))
                                    {
                                        author.Inproceedings.Add(inproceeding.Key);
                                        author.Count = author.Inproceedings.Count;
                                    }
                                }
                                else
                                {
                                    DblpAuthors author = new DblpAuthors(aut.ToString(), inproceeding.StringKey, inproceeding.Key.ToString(), null, 0, 0, fileindex, curline);
                                    dicAuthors.Add(aut, author);
                                }
                            });
                        }
                        lock (lock_conference)
                        {
                            string[] keys = inproceeding.StringKey.Split('/');
                            string conf_key = (keys.Length > 1 ? keys[1] : keys[0]) + "_" + inproceeding.type.ToString();
                            if (dicConferences.ContainsKey(conf_key.GetHashCode()))
                            {
                                DBLPConferences conference = dicConferences[conf_key.GetHashCode()];
                                if (!conference.Years.Contains(inproceeding.Year))
                                {
                                    conference.Years.Add(inproceeding.Year);
                                }
                                if (!conference.InproceedingsByYear.ContainsKey(inproceeding.Year))
                                {
                                    conference.InproceedingsByYear[inproceeding.Year] = new HashSet<int>();
                                    conference.InproceedingsByYear[inproceeding.Year].Add(inproceeding.Key);
                                }
                                else
                                {
                                    if (!conference.InproceedingsByYear[inproceeding.Year].Contains(inproceeding.Key))
                                    {
                                        conference.InproceedingsByYear[inproceeding.Year].Add(inproceeding.Key);
                                    }
                                }
                                conference.CountAll = conference.InproceedingsByYear.Sum(inpr => inpr.Value.Count);
                                inproceeding.Conference = conf_key.GetHashCode();
                            }
                            else
                            {
                                inproceeding.Conference = conf_key.GetHashCode();
                                DBLPConferences conference = new DBLPConferences(conf_key.GetHashCode(), conf_key,
                                    inproceeding.Year, inproceeding.Key, 1, startValue, startValue);
                                dicConferences.Add(conference.Key, conference);
                            }
                        }
                    }
                    if (curline % 100000 == 0)
                    {
                        if (this.IsHandleCreated)
                        {
                            this.Invoke((MethodInvoker)delegate
                            {
                                txtMsgLog.Text += (string.Format("File index ={0} Line index={1} - {2} \r\n", fileindex, curline, x.Name));
                                txtMsgLog.SelectionStart = txtMsgLog.Text.Length;
                                txtMsgLog.ScrollToCaret();
                                if (txtMsgLog.Text.Length > 1000)
                                {
                                    txtMsgLog.Text = "";
                                }
                            });
                        }
                    }
                    curline++;
                }
                //});


            });

        }

        private void SaveAuthors()
        {

            Parallel.For(1, fileAuthors.Count + 1, i =>
            {
                StringBuilder sb = new StringBuilder();

                var dicA = dicAuthors.Where(d => d.Value.ifile == i).OrderBy(d => d.Value.iline);
                dicA.ToList().ForEach(dA =>
                {
                    sb.AppendLine(dA.Key + "~" + dA.Value.ToString());
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

                var dicI = dicInproceedings.Where(d => d.Value.ifile == i).OrderBy(d => d.Value.iline);
                dicI.ToList().ForEach(dI =>
                {
                    sb.AppendLine(dI.Value.ToString());
                });

                File.WriteAllText(Path.GetFullPath(txtFolder.Text + "\\saved-" +
                    txtInproceedingsPrefix.Text + i.ToString() + ".csv"), sb.ToString());
            });
        }
        private void SaveConferences()
        {
            object o = new object();
            StringBuilder sb = new StringBuilder();
            Parallel.ForEach(dicConferences, c =>
            {
                lock (o)
                {
                    sb.AppendLine(c.Value.ToString());
                }
            });
            File.WriteAllText(Path.GetFullPath(txtFolder.Text + "\\saved-" + txtConferencePrefix.Text + ".csv"), sb.ToString());
        }

        private void ParseAuthors()
        {
            int startValue = chkSetStartValue.Checked ? Convert.ToInt32(txtStartValue.Text) : -1;

            Parallel.ForEach(fileAuthors, x =>
            {
                int fileindex = x.Name.LastIndexOf('.');
                fileindex = Convert.ToInt32(x.Name.Substring(fileindex - 1, 1));
                IEnumerable<string> lines;

                lines = File.ReadLines(Path.GetFullPath(x.FullName));

                int curline = 0;
                //Parallel.ForEach(lines, l =>
                foreach (string l in lines)
                {
                    lock (lock_author)
                    {
                        DblpAuthors a = DblpAuthors.CreateFromAuthorsLine(l, curline, fileindex, startValue, fileindex, curline);
                        if (a != null)
                        {
                            if (!dicAuthors.ContainsKey(a.Key))
                            {
                                dicAuthors.Add(a.Key, a);
                            }
                            if (a.Alias != null)
                            {
                                foreach (int i in a.Alias)
                                    if (!dicAuthors.ContainsKey(i))
                                    {
                                        dicAuthors.Add(i, a);
                                    }
                            }
                        }
                    }
                    curline++;
                }
                //});

                this.Invoke((MethodInvoker)delegate
                {
                    txtMsgLog.Text += (string.Format("File index ={0} - {1} \r\n", fileindex, x.Name));
                    txtMsgLog.SelectionStart = txtMsgLog.Text.Length;
                    txtMsgLog.ScrollToCaret();
                    if (txtMsgLog.Text.Length > 1000)
                    {
                        txtMsgLog.Text = "";
                    }
                });
            });


        }

        private void MainFrm_Load(object sender, EventArgs e)
        {
            DirectoryInfo dInfo = new DirectoryInfo(Path.GetFullPath(txtFolder.Text));
            fileArticles = dInfo.GetFiles(txtArticlesPrefix.Text + "*", SearchOption.TopDirectoryOnly).ToList();
            fileInproceedings = dInfo.GetFiles(txtInproceedingsPrefix.Text + "*", SearchOption.TopDirectoryOnly).ToList();
            fileProceedings = dInfo.GetFiles(txtProceedingsPrefix.Text + "*", SearchOption.TopDirectoryOnly).ToList();
            filePhdthesis = dInfo.GetFiles(txtPhdThesisPrefix.Text + "*", SearchOption.TopDirectoryOnly).ToList();
            fileAuthors = dInfo.GetFiles(txtAuthorPrefix.Text + "*", SearchOption.TopDirectoryOnly).ToList();
            fileConferences = dInfo.GetFiles(txtConferencePrefix.Text + "*", SearchOption.TopDirectoryOnly).ToList();
        }

        private void btnTestDBLP_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage5;
            tabPage5.BringToFront();
            currentTestConference = dicConferences.First().Key;

            //txtURLAddress.Text = dicConferences[currentTestConference].Name;
            //wb.Url = new Uri("http://dblp.uni-trier.de/db/conf/" + txtURLAddress.Text);
            Dictionary<string, string> confFull = new Dictionary<string, string>();
            foreach (KeyValuePair<int, DBLPConferences> conf in dicConferences)
            {
                HttpWebRequest request = WebRequest.Create("http://dblp.uni-trier.de/db/conf/" + conf.Value.Name) as HttpWebRequest;
                HttpWebResponse response;
                try
                {
                    response = (HttpWebResponse)request.GetResponse();

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            StreamReader data = new StreamReader(response.GetResponseStream());
                            string result = data.ReadToEnd();
                            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                            doc.LoadHtml(result);
                            var output = doc.DocumentNode.SelectSingleNode("//div[@id='headline']").InnerText;
                            confFull.Add(conf.Value.Name, output);
                        }
                    }
                    else
                    {
                        dicNotConferences.Add(conf.Key, conf.Value.Name);
                    }
                    // Close the response.
                    response.Close();
                }
                catch
                {
                    dicNotConferences.Add(conf.Key, conf.Value.Name);
                }
            }
            txtMsgLog.Text += string.Format("Not conference: {0} - {1}", dicNotConferences.Count,
                dicNotConferences.ToList().ToString());
            dgvConferences.DataSource = confFull.ToList();
        }

        private void btnCalculator_Click(object sender, EventArgs e)
        {
            Thread tr = new Thread(new ThreadStart(ProcessingByYear));
            tr.Start();
        }

        object lock_acal = new object();
        object lock_ical = new object();
        object lock_ccal = new object();

        private void ProcessingByYear()
        {
            int year = Convert.ToInt32(txtBeforeYear.Text);
            int minCountInproceedings = Convert.ToInt32(txtMinCountInproceedings.Text);
            Dictionary<int, DBLPInproceedings> inproCals = dicInproceedings.Where(
              x => (x.Value.Year <= year && dicConferences[x.Value.Conference].CountAll >= minCountInproceedings)).ToDictionary(x => x.Key, x => x.Value);
            dicInproceedings.Clear();

            Dictionary<int, DblpAuthors> authorCals = new Dictionary<int, DblpAuthors>();
            Dictionary<int, DBLPConferences> confCals = new Dictionary<int, DBLPConferences>();

            Parallel.ForEach(inproCals, inproceeding =>
            {

                List<int> authorkeys = inproceeding.Value.Authors.ToList();
                foreach (int ai in authorkeys)
                {
                    if (dicAuthors[ai].Key != ai)
                    {
                        lock (lock_ical)
                        {
                            inproceeding.Value.Authors.Remove(ai);
                            inproceeding.Value.Authors.Add(dicAuthors[ai].Key);
                        }
                    }
                }
            });

            Parallel.ForEach(inproCals, inproceeding =>
            {
                Parallel.ForEach(inproceeding.Value.Authors, ai =>
                {
                    DblpAuthors authorDblp = dicAuthors[ai];
                    lock (lock_acal)
                    {
                        if (!authorCals.Keys.Contains(ai))
                        {
                            authorCals.Add(ai, dicAuthors[ai]);
                            authorCals[ai].Count = 1;
                            authorCals[ai].Inproceedings.Clear();
                            authorCals[ai].Inproceedings.Add(inproceeding.Key);
                            authorCals[ai].CurrentValue = 0;
                            authorCals[ai].OldValue = 0;
                        }
                        else
                        {
                            authorCals[ai].Count++;
                            authorCals[ai].Inproceedings.Add(inproceeding.Key);
                        }
                    }

                });
                lock (lock_ccal)
                {
                    if (!confCals.Keys.Contains(inproceeding.Value.Conference))
                    {
                        confCals.Add(inproceeding.Value.Conference, dicConferences[inproceeding.Value.Conference]);
                        confCals[inproceeding.Value.Conference].CountAll = 1;
                        confCals[inproceeding.Value.Conference].InproceedingsByYear.Clear();
                        confCals[inproceeding.Value.Conference].InproceedingsByYear.Add(inproceeding.Value.Year, new HashSet<int>());
                        confCals[inproceeding.Value.Conference].InproceedingsByYear[inproceeding.Value.Year].Add(inproceeding.Key);

                    }
                    else
                    {
                        confCals[inproceeding.Value.Conference].CountAll++;
                        if (!confCals[inproceeding.Value.Conference].InproceedingsByYear.Keys.Contains(inproceeding.Value.Year))
                        {
                            confCals[inproceeding.Value.Conference].InproceedingsByYear.Add(inproceeding.Value.Year, new HashSet<int>());
                            confCals[inproceeding.Value.Conference].InproceedingsByYear[inproceeding.Value.Year].Add(inproceeding.Key);
                        }
                        else
                        {
                            confCals[inproceeding.Value.Conference].InproceedingsByYear[inproceeding.Value.Year].Add(inproceeding.Key);
                        }
                    }
                }
            });
            dicAuthors.Clear();
            dicConferences.Clear();
            decimal totalValue = Convert.ToDecimal(txtTotalValue.Text);
            decimal startRatio = Convert.ToDecimal(txtStartRatio.Text);
            decimal confRatio = Convert.ToDecimal(txtConferenceRatio.Text);
            ////write Philip S. Yu 692264487
            //DblpAuthors PhilipSYu = authorCals.First(x => x.Key == 692264487).Value;
            //List<DBLPInproceedings> lIPhilips = (from il in inproCals.Values where PhilipSYu.Inproceedings.Contains(il.Key) orderby il.AuthorCount descending select il).ToList();

            //dgvAuthors.DataSource = lIPhilips;
            ////write Harold Vincent Poor 1190337551
            //DblpAuthors VincentPoor = authorCals.First(x => x.Key == 1190337551).Value;
            //List<DBLPInproceedings> lIPoor = (from il in inproCals.Values where VincentPoor.Inproceedings.Contains(il.Key) orderby il.AuthorCount descending select il).ToList();

            //dgvInproceedings.DataSource = lIPoor;
            //return;
            decimal inproStartValue = totalValue / inproCals.Count;
            Parallel.ForEach(inproCals, inproceeding =>
            {
                inproceeding.Value.CurrentValue = inproceeding.Value.OldValue = inproStartValue;
            });
            Dictionary<int, int> authorsCountInproceedings = authorCals.GroupBy(x => x.Value.Key).ToDictionary(y => y.Key, y => y.First().Value.Inproceedings.Count);
            int authorsCount = authorsCountInproceedings.Count;
            int authorsInproceedingsCount = authorsCountInproceedings.Sum(x => x.Value);
            decimal start = totalValue / authorsCount;
            Parallel.ForEach(authorCals, author =>
            {
                author.Value.CurrentValue = author.Value.OldValue = start;
            });
            Parallel.ForEach(confCals, conf =>
            {
                conf.Value.CurrentValue = conf.Value.OldValue = totalValue / confCals.Count;
            });
            Calculating(authorCals, inproCals, confCals, totalValue, startRatio, confRatio, inproStartValue);
            Normalizing(authorCals, inproCals, confCals);
            List<DblpAuthors> lA = (from au in authorCals.Values orderby au.Count descending select au).ToList();
            authorCals.Clear();
            //List<DblpAuthors> lA1 = (from au in lA orderby au.CurrentValue descending select au).ToList();

            //List<DBLPInproceedings> lI = (from il in inproCals.Values orderby il.Count descending select il).ToList();
            //inproCals.Clear();
            //List<DBLPInproceedings> lI1 = (from il in lI orderby il.CurrentValue descending select il).ToList();

            List<DBLPConferences> lC = (from ic in confCals.Values orderby ic.CountAll descending select ic).ToList();
            confCals.Clear();

            WriteValueToFile(txtResultPrefix.Text, lA, inproCals, lC);
            File.WriteAllText(Path.GetFullPath(txtFolder.Text + "\\" + txtResultPrefix.Text + "-" + txtBeforeYear.Text + "-" + (minCountInproceedings).ToString() +
                    "-log.csv"), txtMsgLog.Text);

            if (this.IsHandleCreated)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    txtMsgLog.Text += "Done!!!";
                    this.Cursor = Cursors.Default;

                    dgvAuthors.DataSource = lA.Take(100).ToList();
                    dgvConferences.DataSource = lC;
                    dgvInproceedings.DataSource = inproCals.Values.Take(100).ToList();
                });
            }
        }

        private void Normalizing(Dictionary<int, DblpAuthors> authorCals, Dictionary<int, DBLPInproceedings> inproCals, Dictionary<int, DBLPConferences> confCals)
        {
            List<DblpAuthors> lA = (from au in authorCals.Values orderby au.Count descending select au).Distinct().ToList();
            int count = 0, old_count = 0, old_d_count = 0;
            List<int> ac = new List<int>();
            foreach (DblpAuthors d in lA)
            {
                if (old_d_count != d.Count)
                {
                    count++;
                    old_count = count;
                    old_d_count = d.Count;
                }
                ac.Clear();
                foreach (int ai in authorCals[d.Key].Inproceedings)
                {
                    if (!ac.Contains(inproCals[ai].Conference))
                    {
                        ac.Add(inproCals[ai].Conference);
                    }
                }
                authorCals[d.Key].OldValue = ac.Count; //No of Conference 
                authorCals[d.Key].iline = old_count;//No of PUBs
            }
            lA.Clear();
            lA = (from au in authorCals.Values orderby au.CurrentValue descending select au).Distinct().ToList();
            count = old_count = old_d_count = 0;
            foreach (DblpAuthors d in lA)
            {

                if (old_d_count != d.Count)
                {
                    count++;
                    old_count = count;
                    old_d_count = d.Count;
                }
                authorCals[d.Key].ifile = old_count; //SD4R value
            }
            lA.Clear();
            List<DBLPInproceedings> lI = (from il in inproCals.Values orderby il.AuthorCount descending select il).Distinct().ToList();
            count = old_count = old_d_count = 0;
            foreach (DBLPInproceedings d in lI)
            {

                if (old_d_count != d.AuthorCount)
                {
                    count++;
                    old_count = count;
                    old_d_count = d.AuthorCount;
                }
                inproCals[d.Key].iline = old_count; //No of PUBs
            }
            lI.Clear();
            lI = (from il in inproCals.Values orderby il.CurrentValue descending select il).Distinct().ToList();
            count = old_count = old_d_count = 0;
            foreach (DBLPInproceedings d in lI)
            {

                if (old_d_count != d.AuthorCount)
                {
                    count++;
                    old_count = count;
                    old_d_count = d.AuthorCount;
                }
                inproCals[d.Key].ifile = old_count; //SD4R value
            }
            lI.Clear();
        }

        private void Calculating(Dictionary<int, DblpAuthors> authorCals, Dictionary<int, DBLPInproceedings> inproCals,
            Dictionary<int, DBLPConferences> confCals, decimal totalValue, decimal startRatio, decimal confRatio, decimal inproStartValue)
        {

            if (this.IsHandleCreated)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    txtMsgLog.Text = "Begin...";
                    Cursor = Cursors.WaitCursor;
                });
            }
            Decimal sumAs1 = 0, sumIs1 = 0, sumCs = 0, sumIs2 = 0, sumAs2 = 0, min, ratio, max, max_old = 0, sum0, minstopvalue;
            max = 1000;
            minstopvalue = Convert.ToDecimal(txtMinValue.Text);
            var sw = Stopwatch.StartNew();
            ratio = 1;
            sum0 = inproCals.Sum(x => x.Value.CurrentValue);
            // sumAs1 = authorCals.GroupBy(x => x.Value.Key).ToDictionary(y => y.Key, y => y.First().Value.CurrentValue).Sum(z => z.Value);
            sumAs1 = authorCals.Sum(x => x.Value.CurrentValue);

            while (max > minstopvalue)
            {
                sw.Start();
                authorCals.AsParallel().ForAll(x => x.Value.SetValueFromInproceedings(inproCals));
                //sumAs1 = authorCals.GroupBy(x => x.Value.Key).ToDictionary(y => y.Key, y => y.First().Value.CurrentValue).Sum(z => z.Value);

                sumAs1 = authorCals.Sum(x => x.Value.CurrentValue);
                sumIs1 = inproCals.Sum(x => x.Value.CurrentValue);
                confCals.AsParallel().ForAll(x => x.Value.SetValueFromInproceedings(inproCals));
                sumCs = confCals.Sum(x => x.Value.CurrentValue);
                authorCals.AsParallel().ForAll(x => x.Value.SetValueFromInproceedings(inproCals));
                //sumAs2 = authorCals.GroupBy(x => x.Value.Key).ToDictionary(y => y.Key, y => y.First().Value.CurrentValue).Sum(z => z.Value);

                sumAs2 = authorCals.Sum(x => x.Value.CurrentValue);
                foreach (DBLPInproceedings x in inproCals.Values)
                {
                    x.SetValue(authorCals, confCals, startRatio, confRatio, totalValue, inproStartValue);
                };
                sumIs2 = inproCals.Sum(x => x.Value.CurrentValue);

                ratio = sum0 / sumIs2;
                lock (lock_ical)
                {
                    min = inproCals.Min(x => Math.Abs(x.Value.CurrentValue - x.Value.OldValue));
                    max = inproCals.Max(x => Math.Abs(x.Value.CurrentValue - x.Value.OldValue));
                }
                if (Math.Abs(ratio - 1) > Convert.ToDecimal(0.0009))
                {
                    lock (lock_ical)
                    {
                        Parallel.ForEach(inproCals.Values, x => { x.OldValue = x.CurrentValue; x.CurrentValue = x.CurrentValue * ratio; });
                        sumIs2 = inproCals.Sum(x => x.Value.CurrentValue);
                    }
                }
                if (Math.Abs(max - max_old) > 100000)
                {
                    max_old = max;
                    // WriteResultToFile(max, txtResultPrefix.Text, authorCals, inproCals, confCals);
                    File.WriteAllText(Path.GetFullPath(txtFolder.Text + "\\" + txtResultPrefix.Text + "=" + txtBeforeYear.Text + "-" + ((int)max).ToString() +
                    "-log.csv"), txtMsgLog.Text);
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
            }
            //WriteResultToFile(-1, txtResultPrefix.Text, authorCals, inproCals, confCals);

        }

        private void WriteValueToFile(string prefix, List<DblpAuthors> authorCals, Dictionary<int, DBLPInproceedings> inproCals, List<DBLPConferences> confCals)
        {
            StringBuilder sb = new StringBuilder();
            int count = 0, old_count = 0, old_d_count = 0;

            foreach (DblpAuthors d in authorCals)
            {
                count++;
                if (old_d_count != d.Count)
                {
                    old_count = count;
                    old_d_count = d.Count;
                }
                sb.AppendLine(string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}", d.Key, d.Name, d.Count, d.CurrentValue, d.iline, d.ifile, d.OldValue));
                if (sb.Length > 50000)
                {
                    File.AppendAllText(Path.GetFullPath(txtFolder.Text + "\\" + prefix + ((int)authorCals.Count).ToString() +
                        txtAuthorPrefix.Text + authorCals.Count.ToString() + ".csv"), sb.ToString());
                    sb.Clear();
                }
            }
            File.AppendAllText(Path.GetFullPath(txtFolder.Text + "\\" + prefix + ((int)authorCals.Count).ToString() +
                         txtAuthorPrefix.Text + authorCals.Count.ToString() + ".csv"), sb.ToString());
            sb.Clear();
            count = old_count = old_d_count = 0;
            foreach (KeyValuePair<int, DBLPInproceedings> d in inproCals)
            {
                count++;
                if (old_d_count != d.Value.AuthorCount)
                {
                    old_count = count;
                    old_d_count = d.Value.AuthorCount;
                }
                sb.AppendLine(string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}",
                    d.Value.Key, d.Value.StringKey, d.Value.Conference, d.Value.AuthorCount, d.Value.CurrentValue, d.Value.iline, d.Value.ifile, d.Value.OldValue));

                if (sb.Length > 30000)
                {
                    File.AppendAllText(Path.GetFullPath(txtFolder.Text + "\\" + prefix + ((int)inproCals.Count).ToString() +
                        txtInproceedingsPrefix.Text + inproCals.Count.ToString() + ".csv"), sb.ToString());
                    sb.Clear();
                }
            }
            File.AppendAllText(Path.GetFullPath(txtFolder.Text + "\\" + prefix + ((int)inproCals.Count).ToString() +
                        txtInproceedingsPrefix.Text + inproCals.Count.ToString() + ".csv"), sb.ToString());
            sb.Clear();

            count = old_count = old_d_count = 0;
            List<int> ca = new List<int>();
            foreach (DBLPConferences d in confCals)
            {

                if (old_d_count != d.CountAll)
                {
                    count++;
                    old_count = count;
                    old_d_count = d.CountAll;
                }
                ca.Clear();
                foreach (KeyValuePair<int, HashSet<int>> kvpHi in d.InproceedingsByYear)
                {
                    foreach (int ci in kvpHi.Value)
                    {
                        foreach (int cia in inproCals[ci].Authors)
                        {
                            if (!ca.Contains(cia))
                            {
                                ca.Add(cia);
                            }
                        }
                    }
                }
                sb.AppendLine(string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}", d.Key, d.Name, d.CountAll, d.CurrentValue, old_count, ca.Count));
            }
            File.AppendAllText(Path.GetFullPath(txtFolder.Text + "\\" + prefix + confCals.Count.ToString() +
                        txtConferencePrefix.Text + confCals.Count.ToString() + ".csv"), sb.ToString());
            sb.Clear();
        }

        private void WriteResultToFile(Decimal max, string prefix, Dictionary<int, DblpAuthors> authorCals, Dictionary<int, DBLPInproceedings> inproCals, Dictionary<int, DBLPConferences> confCals)
        {

            StringBuilder sb = new StringBuilder();
            for (int i = 1; i < fileAuthors.Count + 1; i++)
            {
                foreach (KeyValuePair<int, DblpAuthors> d in authorCals)
                {
                    if (d.Value.ifile == i)
                    {
                        sb.AppendLine(d.Value.ToString());
                    }
                    if (sb.Length > 50000)
                    {
                        File.AppendAllText(Path.GetFullPath(txtFolder.Text + "\\" + prefix + ((int)max).ToString() +
                            txtAuthorPrefix.Text + i.ToString() + ".csv"), sb.ToString());
                        sb.Clear();

                    }
                }

                File.AppendAllText(Path.GetFullPath(txtFolder.Text + "\\" + prefix + ((int)max).ToString() +
                    txtAuthorPrefix.Text + i.ToString() + ".csv"), sb.ToString());
                sb.Clear();

            }
            for (int i = 1; i < fileInproceedings.Count + 1; i++)
            {
                foreach (KeyValuePair<int, DBLPInproceedings> d in inproCals)
                {
                    if (d.Value.ifile == i)
                    {
                        sb.AppendLine(d.Value.ToString());
                    }
                    if (sb.Length > 30000)
                    {
                        File.AppendAllText(Path.GetFullPath(txtFolder.Text + "\\" + prefix + ((int)max).ToString() +
                            txtInproceedingsPrefix.Text + i.ToString() + ".csv"), sb.ToString());
                        sb.Clear();
                    }
                }
                File.AppendAllText(Path.GetFullPath(txtFolder.Text + "\\" + prefix + ((int)max).ToString() +
                    txtInproceedingsPrefix.Text + i.ToString() + ".csv"), sb.ToString());
                sb.Clear();
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            for (int i = 1; i < fileConferences.Count + 1; i++)
            {
                foreach (KeyValuePair<int, DBLPConferences> d in confCals)
                {
                    sb.AppendLine(d.Value.ToString());
                }

                File.AppendAllText(Path.GetFullPath(txtFolder.Text + "\\" + prefix + ((int)max).ToString() +
                    txtConferencePrefix.Text + i.ToString() + ".csv"), sb.ToString());
                sb.Clear();
            }
        }

        private void btnMSCalculator_Click(object sender, EventArgs e)
        {
            Thread tr = new Thread(new ThreadStart(ProcessingMicrosoftCompare));
            tr.Start();
        }

        private void ProcessingMicrosoftCompare()
        {
            string[] conflines = File.ReadAllLines(txtFolder.Text + "\\result\\conference list\\conferences.txt");
            Dictionary<int, int> confKey = new Dictionary<int, int>();
            int key;
            int val;
            foreach (string s in conflines)
            {
                string[] keys = s.Split('\t');
                if (int.TryParse(keys[0], out key))
                {
                    if (!int.TryParse(keys[8], out val))
                        val = 0;
                    confKey.Add(key, val);
                }
            }

            int year = Convert.ToInt32(txtBeforeYear.Text);
            Dictionary<int, DBLPInproceedings> inproCals = dicInproceedings.Where(
              x => confKey.Keys.Contains(x.Value.Conference)).ToDictionary(x => x.Key, x => x.Value);
            dicInproceedings.Clear();

            Dictionary<int, DblpAuthors> authorCals = new Dictionary<int, DblpAuthors>();
            Dictionary<int, DBLPConferences> confCals = new Dictionary<int, DBLPConferences>();

            Parallel.ForEach(inproCals, inproceeding =>
            {
                Parallel.ForEach(inproceeding.Value.Authors, ai =>
                {
                    DblpAuthors authorDblp = dicAuthors[ai];
                    lock (lock_acal)
                    {
                        if (!authorCals.Keys.Contains(ai))
                        {
                            authorCals.Add(ai, dicAuthors[ai]);
                            authorCals[ai].Count = 1;
                            authorCals[ai].Inproceedings.Clear();
                            authorCals[ai].Inproceedings.Add(inproceeding.Key);
                            authorCals[ai].CurrentValue = 0;
                            authorCals[ai].OldValue = 0;
                        }
                        else
                        {
                            authorCals[ai].Count++;
                            authorCals[ai].Inproceedings.Add(inproceeding.Key);
                        }
                    }

                });
                lock (lock_ccal)
                {
                    if (!confCals.Keys.Contains(inproceeding.Value.Conference))
                    {
                        confCals.Add(inproceeding.Value.Conference, dicConferences[inproceeding.Value.Conference]);
                        confCals[inproceeding.Value.Conference].CountAll = 1;
                        confCals[inproceeding.Value.Conference].InproceedingsByYear.Clear();
                        confCals[inproceeding.Value.Conference].InproceedingsByYear.Add(inproceeding.Value.Year, new HashSet<int>());
                        confCals[inproceeding.Value.Conference].InproceedingsByYear[inproceeding.Value.Year].Add(inproceeding.Key);

                    }
                    else
                    {
                        confCals[inproceeding.Value.Conference].CountAll++;
                        if (!confCals[inproceeding.Value.Conference].InproceedingsByYear.Keys.Contains(inproceeding.Value.Year))
                        {
                            confCals[inproceeding.Value.Conference].InproceedingsByYear.Add(inproceeding.Value.Year, new HashSet<int>());
                            confCals[inproceeding.Value.Conference].InproceedingsByYear[inproceeding.Value.Year].Add(inproceeding.Key);
                        }
                        else
                        {
                            confCals[inproceeding.Value.Conference].InproceedingsByYear[inproceeding.Value.Year].Add(inproceeding.Key);
                        }
                    }
                }
            });
            dicAuthors.Clear();
            dicConferences.Clear();

            WriteResultToFile(0, "microsoft-", authorCals, inproCals, confCals);
            decimal totalValue = Convert.ToDecimal(txtTotalValue.Text);
            decimal startRatio = Convert.ToDecimal(txtStartRatio.Text);
            decimal confRatio = Convert.ToDecimal(txtConferenceRatio.Text);

            Parallel.ForEach(confCals, conf =>
            {
                conf.Value.CurrentValue = conf.Value.OldValue = confKey[conf.Key] * 1000;
            });

            totalValue = confCals.Sum(x => x.Value.CurrentValue);

            foreach (DBLPInproceedings x in inproCals.Values)
            {
                x.SetValueFromConferences(confCals);
            };
            foreach (DblpAuthors x in authorCals.Values)
            {
                x.SetValueFromInproceedings(inproCals);
            }
            decimal inproStartValue = totalValue / inproCals.Count;

            if (this.IsHandleCreated)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    txtMsgLog.Text = "Begin...";
                    Cursor = Cursors.WaitCursor;
                });
            }
            Decimal sumAs1 = 0, sumIs1 = 0, sumCs = 0, sumIs2 = 0, sumAs2 = 0, min, ratio, max, max_old = 0, sum0;
            max = 1000;
            var sw = Stopwatch.StartNew();
            ratio = 1;
            sum0 = inproCals.Sum(x => x.Value.CurrentValue);
            sumAs1 = authorCals.Sum(x => x.Value.CurrentValue);

            while (max > Convert.ToDecimal(txtMinValue.Text))
            {
                sw.Start();
                authorCals.AsParallel().ForAll(x => x.Value.SetValueFromInproceedings(inproCals));
                sumAs1 = authorCals.Sum(x => x.Value.CurrentValue);
                sumIs1 = inproCals.Sum(x => x.Value.CurrentValue);
                confCals.AsParallel().ForAll(x => x.Value.SetValueFromInproceedings(inproCals));
                sumCs = confCals.Sum(x => x.Value.CurrentValue);
                authorCals.AsParallel().ForAll(x => x.Value.SetValueFromInproceedings(inproCals));
                sumAs2 = authorCals.Sum(x => x.Value.CurrentValue);
                foreach (DBLPInproceedings x in inproCals.Values)
                {
                    x.SetValue(authorCals, confCals, startRatio, confRatio, totalValue, inproStartValue);
                };
                sumIs2 = inproCals.Sum(x => x.Value.CurrentValue);

                ratio = sum0 / sumIs2;
                min = inproCals.Min(x => Math.Abs(x.Value.CurrentValue - x.Value.OldValue));
                max = inproCals.Max(x => Math.Abs(x.Value.CurrentValue - x.Value.OldValue));

                if (Math.Abs(ratio - 1) > Convert.ToDecimal(0.009))
                {
                    Parallel.ForEach(inproCals.Values, x => { x.OldValue = x.CurrentValue; x.CurrentValue = x.CurrentValue * ratio; });
                }
                if (Math.Abs(max - max_old) > 1000)
                {
                    max_old = max;
                    //WriteResultToFile(max, txtResultPrefix.Text, authorCals, inproCals, confCals);
                    File.WriteAllText(Path.GetFullPath(txtFolder.Text + "\\" + txtResultPrefix.Text + "=" + txtBeforeYear.Text + "-" + ((int)max).ToString() +
                    "-log.csv"), txtMsgLog.Text);
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
            }
            WriteResultToFile(-1, txtResultPrefix.Text, authorCals, inproCals, confCals);
            File.WriteAllText(Path.GetFullPath(txtFolder.Text + "\\" + txtResultPrefix.Text + "=" + txtBeforeYear.Text + "-" + ((int)max).ToString() +
                    "-log.csv"), txtMsgLog.Text);
            if (this.IsHandleCreated)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    txtMsgLog.Text += "Done!!!";
                    this.Cursor = Cursors.Default;
                    List<DblpAuthors> lA = (from au in authorCals.Values orderby au.CurrentValue descending select au).Take(10000).ToList();
                    authorCals.Clear();
                    List<DBLPInproceedings> lI = (from il in inproCals.Values orderby il.CurrentValue descending select il).Take(10000).ToList();
                    inproCals.Clear();
                    dgvAuthors.DataSource = lA;
                    dgvConferences.DataSource = confCals.Values.ToList();
                    dgvInproceedings.DataSource = lI;
                });
            }
        }

        private void btnLoadCitation_Click(object sender, EventArgs e)
        {
            //string[] a = File.ReadAllLines(Path.Combine(txtFolder.Text, txtCitationsFile.Text));
            string strTitle = (txtFileVersion.Text == "5" ? "#*" : "#*");
            string strAuthors = (txtFileVersion.Text == "5" ? "#@" : "#@");
            string strYear = (txtFileVersion.Text == "5" ? "#year" : "#t");
            string strConf = (txtFileVersion.Text == "5" ? "#conf" : "#c");
            string strCitationNumber = (txtFileVersion.Text == "5" ? "#citation" : "");
            string strIndex = (txtFileVersion.Text == "5" ? "#index" : "#index");
            string strArnetid = (txtFileVersion.Text == "5" ? "#arnetid" : "");
            string strCitationid = (txtFileVersion.Text == "5" ? "#%" : "#%");
            string atrAbstract = (txtFileVersion.Text == "5" ? "#!" : "#!");
            string textAuthor = "";
            string textYear = "";
            string textConf = "";
            string textCitationCount = "";


            Dictionary<int, DBLPInproceedings> inproCitations = new Dictionary<int, DBLPInproceedings>();
            Dictionary<int, DblpAuthors> authorCitations = new Dictionary<int, DblpAuthors>();
            Dictionary<int, DBLPConferences> confCitations = new Dictionary<int, DBLPConferences>();
            DblpAuthors curAuthor = null;
            DBLPInproceedings curInproceedings = null;
            DBLPConferences curConf = null;
            StringBuilder sb = new StringBuilder();

            using (StreamReader reader = new StreamReader(Path.Combine(txtFolder.Text, txtCitationsFile.Text)))
            {
                string nextline = reader.ReadLine();

                string textline = null;
                int count = 0;
                int lineInproceedings = 1;
                while (nextline != null)
                {
                    if (nextline == "" || int.TryParse(nextline, out count)) //begin record
                    {
                        lineInproceedings++;
                        if (lineInproceedings % 100 == 0)
                        {
                            File.AppendAllText(Path.GetFullPath(txtFolder.Text + "\\" +
                                txtCitationsFile.Text + "-" + (lineInproceedings / 300000).ToString() + ".csv"), sb.ToString());
                            sb.Clear();
                        }
                        //save old inproceedings
                        if (curInproceedings != null)
                        {
                            inproCitations.Add(curInproceedings.Key, curInproceedings);
                        }
                        //prepare inproceedings
                        curInproceedings = new DBLPInproceedings();
                    }
                    else if (nextline.StartsWith(strTitle))  //title
                    {
                        textline = nextline.Substring(strTitle.Length);
                        curInproceedings.Title = textline;
                    }
                    else if (nextline.StartsWith(strAuthors))  //authors
                    {
                        textAuthor = nextline.Substring(strAuthors.Length);
                    }
                    else if (nextline.StartsWith(strYear))  //year
                    {
                        textYear = nextline.Substring(strYear.Length);
                    }
                    else if (nextline.StartsWith(strConf))  //conf
                    {
                        textConf = nextline.Substring(strConf.Length);
                    }
                    else if (nextline.StartsWith(strIndex))  //Index
                    {
                        int confId = textConf.GetHashCode();
                        textline = nextline.Substring(strIndex.Length);
                        curInproceedings.Key = Convert.ToInt32(textline);
                        curInproceedings.Conference = confId;
                        curInproceedings.Year = Convert.ToInt32(textYear);
                        if (confCitations.Keys.Contains(confId))
                        {
                            curConf = confCitations[confId];
                            curConf.CountAll++;
                            if (!curConf.InproceedingsByYear.Keys.Contains(curInproceedings.Year))
                            {
                                curConf.InproceedingsByYear.Add(curInproceedings.Year, new HashSet<int>());
                                curConf.InproceedingsByYear[curInproceedings.Year].Add(curInproceedings.Key);
                            }
                            else
                            {
                                curConf.InproceedingsByYear[curInproceedings.Year].Add(curInproceedings.Key);
                            }
                        }
                        else
                        {
                            curConf = new DBLPConferences(confId, textConf, curInproceedings.Year, curInproceedings.Key, 1, 0, 0);
                            confCitations.Add(curConf.Key, curConf);
                        }
                        //add authors to id of inproceedings
                        string[] authorstext = textAuthor.Split(',');
                        int a_id;
                        curInproceedings.Authors = new HashSet<int>();
                        foreach (string a in authorstext)
                        {
                            a_id = a.GetHashCode();
                            if (authorCitations.Keys.Contains(a_id))
                            {
                                curAuthor = authorCitations[a_id];
                                curAuthor.Count++;
                                curAuthor.Inproceedings.Add(curInproceedings.Key);
                            }
                            else
                            {
                                curAuthor = new DblpAuthors(a_id.ToString(), a, curInproceedings.Key.ToString());
                                authorCitations.Add(curAuthor.Key, curAuthor);
                            }
                            curInproceedings.Authors.Add(curAuthor.Key);
                            curInproceedings.AuthorCount++;
                        }
                        //add citations count
                        curInproceedings.CitationCount = Convert.ToInt32(textCitationCount == "" ? "0" : textCitationCount);
                    }
                    else if (nextline.StartsWith(strCitationid))  //Citationid
                    {
                        textline = nextline.Substring(strCitationid.Length);
                        if (curInproceedings.CitationIds == null)
                            curInproceedings.CitationIds = new HashSet<int>();
                        curInproceedings.CitationIds.Add(Convert.ToInt32(textline));
                    }
                    else if (nextline.StartsWith(atrAbstract))  //Abstract
                    {
                        textline = nextline.Substring(atrAbstract.Length);
                    }
                    else if (nextline.StartsWith(strCitationNumber))  //CitationNumber
                    {
                        textCitationCount = nextline.Substring(strCitationNumber.Length);
                    }
                    else if (nextline.StartsWith(strArnetid))  //Arnetid
                    {
                        textline = nextline.Substring(strArnetid.Length);
                    }
                    sb.AppendLine(nextline);
                    nextline = reader.ReadLine();


                }
                File.AppendAllText(Path.GetFullPath(txtFolder.Text + "\\" +
                              txtCitationsFile.Text + "-" + (lineInproceedings / 300000).ToString() + ".csv"), sb.ToString());
                sb.Clear();
            }




            foreach (KeyValuePair<int, DblpAuthors> d in authorCitations)
            {
                sb.AppendLine(d.Value.ToString());
                if (sb.Length > 50000)
                {
                    File.AppendAllText(Path.GetFullPath(txtFolder.Text + "\\" +
                        txtCiteAuthor.Text + ".csv"), sb.ToString());
                    sb.Clear();
                }
            }

            File.AppendAllText(Path.GetFullPath(txtFolder.Text + "\\" +
                         txtCiteAuthor.Text + ".csv"), sb.ToString());
            sb.Clear();

            foreach (KeyValuePair<int, DBLPInproceedings> d in inproCitations)
            {
                sb.AppendLine(d.Value.ToString());
                if (sb.Length > 50000)
                {
                    File.AppendAllText(Path.GetFullPath(txtFolder.Text + "\\" +
                        txtCiteInproceedings.Text + ".csv"), sb.ToString());
                    sb.Clear();
                }
            }

            File.AppendAllText(Path.GetFullPath(txtFolder.Text + "\\" +
                         txtCiteInproceedings.Text + ".csv"), sb.ToString());
            sb.Clear();

            foreach (KeyValuePair<int, DBLPConferences> d in confCitations)
            {
                sb.AppendLine(d.Value.ToString());
                if (sb.Length > 50000)
                {
                    File.AppendAllText(Path.GetFullPath(txtFolder.Text + "\\" +
                        txtCiteProceedings.Text + ".csv"), sb.ToString());
                    sb.Clear();
                }
            }

            File.AppendAllText(Path.GetFullPath(txtFolder.Text + "\\" +
                         txtCiteProceedings.Text + ".csv"), sb.ToString());
            sb.Clear();
        }

        private void btnTestIEEE_Click(object sender, EventArgs e)
        {
            txtLogCitations.Text = "Start...\r\n";
            Thread tr = new Thread(new ThreadStart(CrawlerIEEE));
            tr.Start();
        }

        private void CrawlerIEEE()
        {
            var sw = Stopwatch.StartNew();
            int start = Convert.ToInt32(txtStartNumber.Text);
            int stop = Convert.ToInt32(txtStopNumber.Text);
            int loopstep = Convert.ToInt32(txtRestartTimes.Text);
            int timeout = Convert.ToInt32(txtTimeout.Text);
            object lock_write = new object();
            string folder = txtFolder.Text;
            string strarticleDetails = txtarticleDetails.Text;
            string strabstractAuthors = txtabstractAuthors.Text;
            string strabstractReferences = txtabstractReferences.Text;
            string strabstractCitations = txtabstractCitations.Text;
            Parallel.For(start, stop, (i) =>
            {
                StringBuilder sb = new StringBuilder();
                
                HttpWebRequest request = WebRequest.Create(strarticleDetails + i.ToString()) as HttpWebRequest;
                HttpWebResponse response;
                var title="";
                bool error = false;
                #region article
                bool restart = false;
                int step = 0;
                do
                {
                    step++;
                    try
                    {
                        Thread.Sleep(3000);
                        request.Timeout = timeout;
                        response = (HttpWebResponse)request.GetResponse();

                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            if (response.StatusCode == HttpStatusCode.OK)
                            {
                                StreamReader data = new StreamReader(response.GetResponseStream());
                                string result = data.ReadToEnd();
                                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                                doc.LoadHtml(result);
                                title = doc.DocumentNode.SelectSingleNode("//title").InnerText;
                                if (title != "IEEE Xplore - Error Page")
                                {
                                    var article_detail = doc.DocumentNode.SelectSingleNode("//div[@class='article-ftr']").InnerHtml;
                                    sb.AppendFormat("<div class='pub'>{0}</div>\r\n<div class='title'>{1}</div>\r\n<div class='article-ftr'>{2}</div>\r\n", i, title, article_detail);
                                    //File.AppendAllText(Path.GetFullPath(folder + "\\ieee-pubs-" +
                                    //(i).ToString() + ".html"), string.Format("<div class='pub'>{0}</div>\r\n<div class='title'>{1}</div>\r\n<div class='article-ftr'>{2}</div>\r\n", i, title, article_detail));
                                }
                                else
                                {
                                    error = true;
                                }
                            }
                        }
                        // Close the response.
                        response.Close();
                        restart = false;
                    }
                    catch (WebException ex)
                    {
                        if (ex.Message == "The operation has timed out")
                            restart = true;
                        if (this.IsHandleCreated)
                        {
                            this.Invoke((MethodInvoker)delegate
                            {
                                txtLogCitations.Text += string.Format("<div class='log'>Thread time: ({0}:{1}:{2}) - ID={3} -ARTICLE TIMEOUT -restart: {4}</div>\r\n",
                                    sw.Elapsed.Hours, sw.Elapsed.Minutes, sw.Elapsed.Seconds, i, step);
                                txtLogCitations.SelectionStart = txtLogCitations.Text.Length;
                                txtLogCitations.ScrollToCaret();
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        if (this.IsHandleCreated)
                        {
                            this.Invoke((MethodInvoker)delegate
                            {
                                txtLogCitations.Text += string.Format("<div class='log'>Thread time: ({0}:{1}:{2}) - ID={3} -Error NO ARTICLE:{4}</div>\r\n",
                                    sw.Elapsed.Hours, sw.Elapsed.Minutes, sw.Elapsed.Seconds, i, ex.Message);
                                txtLogCitations.SelectionStart = txtLogCitations.Text.Length;
                                txtLogCitations.ScrollToCaret();
                            });
                        }
                    }
                }
                while (restart && (step < loopstep));
                #endregion article

                if (!error)
                {
                    #region abstractAuthor
                    restart = false;
                    step = 0;
                    do
                    {
                        step++;
                        
                        Thread.Sleep(2000);
                        request = WebRequest.Create(strabstractAuthors + i.ToString()) as HttpWebRequest;
                        try
                        {
                            request.Timeout = timeout;
                            response = (HttpWebResponse)request.GetResponse();

                            if (response.StatusCode == HttpStatusCode.OK)
                            {
                                if (response.StatusCode == HttpStatusCode.OK)
                                {
                                    StreamReader data = new StreamReader(response.GetResponseStream());
                                    string result = data.ReadToEnd();
                                    HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                                    doc.LoadHtml(result);
                                    title = doc.DocumentNode.SelectSingleNode("//title").InnerText;
                                    if (title != "IEEE Xplore - Error Page")
                                    {
                                        var article_authors = doc.DocumentNode.SelectSingleNode("//div[@class='art-authors']").InnerHtml;
                                        sb.AppendFormat("<div class='art-authors'>{0}</div>\r\n", article_authors);
                                        //File.AppendAllText(Path.GetFullPath(folder + "\\ieee-pubs-" +
                                        //(i).ToString() + ".html"), string.Format("<div class='art-authors'>{0}</div>\r\n", article_authors));
                                    }
                                }
                            }
                            // Close the response.
                            response.Close();
                            restart = false;
                        }                  // "The operation has timed out"    
                        catch (WebException ex)
                        {
                            if (ex.Message=="The operation has timed out")
                                restart = true;
                            if (this.IsHandleCreated)
                            {
                                this.Invoke((MethodInvoker)delegate
                                {
                                    txtLogCitations.Text += string.Format("<div class='log'>Thread time: ({0}:{1}:{2}) - ID={3} -AUTHORS TIMEOUT -restart: {4}</div>\r\n",
                                        sw.Elapsed.Hours, sw.Elapsed.Minutes, sw.Elapsed.Seconds, i,step);
                                    txtLogCitations.SelectionStart = txtLogCitations.Text.Length;
                                    txtLogCitations.ScrollToCaret();
                                });
                            }
                        }
                        catch (Exception ex)
                        {      
                            if (this.IsHandleCreated)
                            {
                                this.Invoke((MethodInvoker)delegate
                                {
                                    txtLogCitations.Text += string.Format("<div class='log'>Thread time: ({0}:{1}:{2}) - ID={3} -Error NO AUTHORS</div>\r\n",
                                        sw.Elapsed.Hours, sw.Elapsed.Minutes, sw.Elapsed.Seconds, i);
                                    txtLogCitations.SelectionStart = txtLogCitations.Text.Length;
                                    txtLogCitations.ScrollToCaret();
                                });
                            }
                        }
                        
                    } while (restart && (step<loopstep));
                    #endregion abstractAuthor

                    #region abstractReferences
                    step =0;
                    restart = false;
                    do
                    {
                        step++;                       
                        Thread.Sleep(2000);
                        request = WebRequest.Create(strabstractReferences + i.ToString()) as HttpWebRequest;
                        try
                        {
                            request.Timeout = timeout;
                            response = (HttpWebResponse)request.GetResponse();

                            if (response.StatusCode == HttpStatusCode.OK)
                            {
                                if (response.StatusCode == HttpStatusCode.OK)
                                {
                                    StreamReader data = new StreamReader(response.GetResponseStream());
                                    string result = data.ReadToEnd();
                                    HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                                    doc.LoadHtml(result);
                                    title = doc.DocumentNode.SelectSingleNode("//title").InnerText;
                                    if (title != "IEEE Xplore - Error Page")
                                    {
                                        var article_refs = doc.DocumentNode.SelectSingleNode("//ol[@class='docs']").InnerHtml;
                                        sb.AppendFormat("<div class='ref'><ol id='references' class='docs'>{0}</ol></div>\r\n", article_refs);
                                        //File.AppendAllText(Path.GetFullPath(folder + "\\ieee-pubs-" +
                                        //(i).ToString() + ".html"), string.Format("<div class='ref'><ol id='references' class='docs'>{0}</ol></div>\r\n", article_refs));
                                    }
                                }
                            }
                            // Close the response.
                            response.Close();
                            restart = false;
                        }
                        catch (WebException ex)
                        {
                            if (ex.Message == "The operation has timed out")
                                restart = true;
                            if (this.IsHandleCreated)
                            {
                                this.Invoke((MethodInvoker)delegate
                                {
                                    txtLogCitations.Text += string.Format("<div class='log'>Thread time: ({0}:{1}:{2}) - ID={3} -REF TIMEOUT -restart: {4}</div>\r\n",
                                        sw.Elapsed.Hours, sw.Elapsed.Minutes, sw.Elapsed.Seconds, i, step);
                                    txtLogCitations.SelectionStart = txtLogCitations.Text.Length;
                                    txtLogCitations.ScrollToCaret();
                                });
                            }
                        }
                        catch (Exception ex)
                        {
                            if (this.IsHandleCreated)
                            {
                                this.Invoke((MethodInvoker)delegate
                                {
                                    txtLogCitations.Text += string.Format("Thread time: ({0}:{1}:{2}) - ID={3} -Error NO REF\r\n",
                                        sw.Elapsed.Hours, sw.Elapsed.Minutes, sw.Elapsed.Seconds, i);
                                    txtLogCitations.SelectionStart = txtLogCitations.Text.Length;
                                    txtLogCitations.ScrollToCaret();
                                });
                            }
                        }

                    } while (restart && (step < loopstep));
                    #endregion abstractReferences
                    //Ieee_citations
                    //NonIeee_citations
                    //Patent_citations
                    #region citations
                    step =0;
                    restart = false;
                    do
                    {
                        step++;
                        Thread.Sleep(2000);
                        request = WebRequest.Create(strabstractCitations + i.ToString()) as HttpWebRequest;
                        try
                        {
                            request.Timeout = timeout;
                            response = (HttpWebResponse)request.GetResponse();

                            if (response.StatusCode == HttpStatusCode.OK)
                            {
                                if (response.StatusCode == HttpStatusCode.OK)
                                {
                                    StreamReader data = new StreamReader(response.GetResponseStream());
                                    string result = data.ReadToEnd();
                                    HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                                    doc.LoadHtml(result);
                                    title = doc.DocumentNode.SelectSingleNode("//title").InnerText;
                                    if (title != "IEEE Xplore - Error Page")
                                    {

                                        try
                                        {
                                            var article_refs = doc.DocumentNode.SelectSingleNode("//ol[@id='Ieee_citations']").InnerHtml;
                                            sb.AppendFormat("<div class='Ieee_citations'><ol id='Ieee_citations' class='docs'>{0}</ol></div>\r\n", article_refs);
                                            //File.AppendAllText(Path.GetFullPath(folder + "\\ieee-pubs-" +
                                            //    (i).ToString() + ".html"), string.Format("<div><ol id='Ieee_citations' class='docs'>{0}</ol></div>\r\n", article_refs));
                                        }
                                        catch (Exception ex)
                                        {
                                            if (this.IsHandleCreated)
                                            {
                                                this.Invoke((MethodInvoker)delegate
                                                {
                                                    txtLogCitations.Text += string.Format("<div class='log'>Thread time: ({0}:{1}:{2}) - ID={3} -Error NO Ieee_citations</div>\r\n",
                                                        sw.Elapsed.Hours, sw.Elapsed.Minutes, sw.Elapsed.Seconds, i);
                                                    txtLogCitations.SelectionStart = txtLogCitations.Text.Length;
                                                    txtLogCitations.ScrollToCaret();
                                                });
                                            }
                                        }
                                        try
                                        {
                                            var article_refs = doc.DocumentNode.SelectSingleNode("//ol[@id='NonIeee_citations']").InnerHtml;
                                            sb.AppendFormat("<div class='NonIeee_citations'><ol id='NonIeee_citations' class='docs'>{0}</ol></div>\r\n", article_refs);
                                            // File.AppendAllText(Path.GetFullPath(folder + "\\ieee-pubs-" +
                                            //(i).ToString() + ".html"), string.Format("<div><ol id='NonIeee_citations' class='docs'>{0}</ol></div>\r\n", article_refs));
                                        }
                                        catch (Exception ex)
                                        {
                                            if (this.IsHandleCreated)
                                            {
                                                this.Invoke((MethodInvoker)delegate
                                                {
                                                    txtLogCitations.Text += string.Format("<div class='log'>Thread time: ({0}:{1}:{2}) - ID={3} -Error NO NonIeee_citations</div>\r\n",
                                                        sw.Elapsed.Hours, sw.Elapsed.Minutes, sw.Elapsed.Seconds, i);
                                                    txtLogCitations.SelectionStart = txtLogCitations.Text.Length;
                                                    txtLogCitations.ScrollToCaret();
                                                });
                                            }
                                        }
                                        try
                                        {
                                            var article_refs = doc.DocumentNode.SelectSingleNode("//ol[@id='Patent_citations']").InnerHtml;
                                            sb.AppendFormat("<div class='Patent_citations'><ol id='Patent_citations' class='docs'>{0}</ol></div>\r\n", article_refs);
                                            //File.AppendAllText(Path.GetFullPath(folder + "\\ieee-pubs-" +
                                            //  (i).ToString() + ".html"), string.Format("<div><ol id='Patent_citations' class='docs'>{0}</ol></div>\r\n", article_refs));
                                        }
                                        catch (Exception ex)
                                        {
                                            if (this.IsHandleCreated)
                                            {
                                                this.Invoke((MethodInvoker)delegate
                                                {
                                                    txtLogCitations.Text += string.Format("<div class='log'>Thread time: ({0}:{1}:{2}) - ID={3} -Error NO Patent_citations</div>\r\n",
                                                        sw.Elapsed.Hours, sw.Elapsed.Minutes, sw.Elapsed.Seconds, i);
                                                    txtLogCitations.SelectionStart = txtLogCitations.Text.Length;
                                                    txtLogCitations.ScrollToCaret();
                                                });
                                            }
                                        }
                                    }
                                }
                            }
                            // Close the response.
                            response.Close();
                            restart = false;
                        }
                        catch (WebException ex)
                        {
                            if (ex.Message == "The operation has timed out")
                                restart = true;
                            if (this.IsHandleCreated)
                            {
                                this.Invoke((MethodInvoker)delegate
                                {
                                    txtLogCitations.Text += string.Format("<div class='log'>Thread time: ({0}:{1}:{2}) - ID={3} -CITATIONS TIMEOUT -restart: {4}</div>\r\n",
                                        sw.Elapsed.Hours, sw.Elapsed.Minutes, sw.Elapsed.Seconds, i, step);
                                    txtLogCitations.SelectionStart = txtLogCitations.Text.Length;
                                    txtLogCitations.ScrollToCaret();
                                });
                            }
                        }
                        catch (Exception ex)
                        {
                            if (this.IsHandleCreated)
                            {
                                this.Invoke((MethodInvoker)delegate
                                {
                                    txtLogCitations.Text += string.Format("<div class='log'>Thread time: ({0}:{1}:{2}) - ID={3} -Error:{4}</div>\r\n",
                                        sw.Elapsed.Hours, sw.Elapsed.Minutes, sw.Elapsed.Seconds, i, ex.Message);
                                    txtLogCitations.SelectionStart = txtMsgLog.Text.Length;
                                    txtLogCitations.ScrollToCaret();
                                });
                            }
                        }
                    } while (restart && (step < loopstep));
                    #endregion citations
                }
                if (sb.Length > 0)
                {
                    //lock (lock_write)
                    {
                        File.AppendAllText(Path.GetFullPath(folder + "\\ieee-pubs-" +
                                (i).ToString() + ".html"), "<head></head><body>" + sb.ToString() + "</body>");
                        sb.Clear();
                    }                    
                }
                if (this.IsHandleCreated)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        if (txtLogCitations.Text.Length > 50000)
                        {
                            File.AppendAllText(Path.GetFullPath(folder + "\\log-ieee-pubs-" +
                               start.ToString() + "-" + stop.ToString() + ".html"), txtLogCitations.Text);
                            txtLogCitations.Text = "";
                        }

                        txtLogCitations.Text += string.Format("<div class='log'>Thread time: ({0}:{1}:{2}) - ID={3} - DONE</div>\r\n ",
                            sw.Elapsed.Hours, sw.Elapsed.Minutes, sw.Elapsed.Seconds, i);
                        txtLogCitations.SelectionStart = txtLogCitations.Text.Length;
                        txtLogCitations.ScrollToCaret();
                    });
                }
                
            });
            if (this.IsHandleCreated)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    txtLogCitations.Text += string.Format("Done!!!");
                    txtLogCitations.SelectionStart = txtLogCitations.Text.Length;
                    txtLogCitations.ScrollToCaret();
                    File.AppendAllText(Path.GetFullPath(folder + "\\log-ieee-pubs-" +
                               start.ToString() + "-" + stop.ToString() + ".html"), txtLogCitations.Text);
                });
            }
        }
    }
}
