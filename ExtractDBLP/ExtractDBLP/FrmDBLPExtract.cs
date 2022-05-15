namespace ExtractDBLPForm
{
    using MessagePack;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Windows.Forms;

    public partial class FrmDBLPExtract : Form
    {
        public FrmDBLPExtract()
        {
            InitializeComponent();
            this.txtDBLPfile.Text = @"C:\Data\dblp\dblp.xml";
        }

        private void FrmDBLPExtract_Load(object sender, EventArgs e)
        {
            //var lz4Options = MessagePackSerializerOptions.Standard.WithCompression(MessagePackCompression.Lz4BlockArray);
            //var fw = MessagePackSerializer.Deserialize<ExportPaper[]>(File.ReadAllBytes(@"..\..\data.bin"), lz4Options);
            ////var lz4Options2 = MessagePackSerializerOptions.Standard.WithCompression(MessagePackCompression.Lz4Block);
            //var old = MessagePackSerializerOptions.Standard.WithOldSpec();
            //var data = MessagePackSerializer.Serialize(fw, old);
            //File.WriteAllBytes(@"..\..\paper.bin", data);
            //var json = JsonConvert.SerializeObject(
            //    new { records = fw, },
            //    Newtonsoft.Json.Formatting.Indented,
            //    new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            //File.WriteAllText(@"..\..\data2.json", json);
        }

        private void btnStart_Click(object sender, EventArgs eventarg)
        {
            //var keywords = "blockchain,merkle,bitcoin,ethereum,hyperledger,monero,eosio,algorand,zcash,filecoin,immutable";
            var keywords = "sgx,trusted execution,privacy,federat,enclave,trustzone,amd sev";
            //var yearstart = 2018;
            var words = keywords
                .Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                .Select(_ => _.Trim())
                .ToArray();
            var wordStats = words.ToDictionary(_ => _, _ => 0);

            var rs = DblpParser.GetRecords(this.txtDBLPfile.Text);
            //var fw = FilterByWords(rs, words, yearstart, wordStats)
            var yearstart = 2011;
            var yearend = 2023;
            var dbaclass = new[] {
                "journals/tods/",
                "journals/tois/",
                "journals/tkde/",
                "journals/vldb/",
                "conf/sigmod/",
                "conf/kdd/",
                "conf/icde/",
                "conf/sigir/",
                "conf/vldb/",
            };
            var dbbclass = new[] {
                "journals/tkdd/", "journals/tweb/", "journals/aei/", "journals/dke/", "journals/datamine/", "journals/ejis/", "journals/geoinformatica/", "journals/ipm/", "journals/isci/", "journals/is/", "journals/jasis/", "journals/ws/", "journals/kais/", "conf/cikm/", "conf/wsdm/", "conf/pods/", "conf/dasfaa/", "conf/ecml/", "conf/semweb/", "conf/icdm/", "conf/icdt/", "conf/edbt/", "conf/cidr/", "conf/sdm/"
            };
            var dbcclass = new[] {
                "journals/dpd/", "journals/iam/", "journals/ipl/", "journals/ir/", "journals/ijcis/", "journals/gis/", "journals/ijis/", "journals/ijkm/", "journals/ijswis/", "journals/jcis/", "journals/jdm/", "journals/jiis/", "journals/jsis/", "conf/apweb/", "conf/dexa/", "conf/ecir/", "conf/esws/", "conf/webdb/", "conf/er/", "conf/mdm/", "conf/ssdbm/", "conf/waim/", "conf/ssd/", "conf/pakdd/", "conf/wise/"
            };
            var otherclass = new string[] {
                "journals/pvldb/",
            };
            var clses = new[] { dbaclass, dbbclass, dbcclass, otherclass }.SelectMany(_ => _).ToArray();
            var fw = FilterByKeyPrefix(rs, clses, (yearstart, yearend))
                .Select(_ => new ExportPaper(_))
                .ToArray();

            var lz4Options = MessagePackSerializerOptions.Standard.WithCompression(MessagePackCompression.Lz4BlockArray);
            var data = MessagePackSerializer.Serialize(fw, lz4Options);
            File.WriteAllBytes(@"..\..\data.bin", data);


            //var json = JsonConvert.SerializeObject(
            //    new { records = fw, stats = wordStats, filename = Path.GetFileName(this.txtDBLPfile.Text) },
            //    Newtonsoft.Json.Formatting.Indented,
            //    new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            //File.WriteAllText(@"..\..\papers.js", "var papers = " + json);
        }

        private IEnumerable<DblpRecord> FilterByKeyPrefix(IEnumerable<DblpRecord> records, string[] keyPrefixes, (int start, int end) year)
        {
            var i = 0;
            var p = 0;
            foreach (var record in records)
            {
                i++;
                var isMatch = false;

                if (keyPrefixes.Any(_ => record.key.StartsWith(_)))
                    isMatch = true;

                // filter year
                if (isMatch && (record is Paper pp && (!int.TryParse(pp.year, out var y) || (y < year.start || y > year.end))))
                {
                    isMatch = false;
                }

                if (isMatch)
                {
                    p++;
                    yield return record;
                }

                //if (p > 100) yield break;

                this.UpdateProgress(i, p);
            }

            this.UpdateProgress(i, p, true);
        }
        private IEnumerable<DblpRecord> FilterByWords(IEnumerable<DblpRecord> records, string[] words, int yearstart, Dictionary<string, int> wordStats)
        {
            var i = 0;
            var p = 0;
            foreach (var record in records)
            {
                i++;
                var isMatch = false;
                // filter words
                foreach (var word in words)
                {
                    if (record.title.IndexOf(word, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        isMatch = true;

                        // filter year
                        if (isMatch && (record is Paper pp && (!int.TryParse(pp.year, out var y) || y < yearstart)))
                        {
                            isMatch = false;
                        }

                        if (isMatch) wordStats[word]++;
                    }
                }

                if (isMatch)
                {
                    p++;
                    yield return record;
                }

                this.UpdateProgress(i, p);
            }

            this.UpdateProgress(i, p, true);
        }

        private void UpdateProgress(int totalProcessed, int found, bool isFinished = false)
        {
            if (!isFinished && totalProcessed % 10000 != 0) return;

            Application.DoEvents();
            var p = found;
            var i = totalProcessed;
            var progTick = 100_0000;

            this.Invoke((Action)(() =>
            {
                if (isFinished)
                {
                    this.lblStatus.Text = $"Finished [{p}/{i}]";
                    this.barProgress.Maximum = 100;
                    this.barProgress.Value = 100;
                }
                else
                {
                    this.lblStatus.Text = $"Processing {p}/{i} items";
                    var m = (i / progTick) + 1;
                    m = Math.Max(m, 9);
                    this.barProgress.Maximum = m * progTick;
                    this.barProgress.Value = i;
                }
            }));
        }
    }
}