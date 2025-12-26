namespace ExtractDBLPForm;

using ExtractDBLPForm.Exporters;
using ExtractDBLPForm.Models;
using ExtractDBLPForm.Parsers;
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
        //this.txtDBLPfile.Text = @"C:\Data\dblp\dblp.xml";
        // download from https://dblp.uni-trier.de/xml/
        this.txtDBLPfile.Text = @"D:\LargeWork\dblp\origin_data\dblp.xml";
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
        this.cmbKeyPrefix.Items.Clear();
        this.cmbKeyPrefix.Items.AddRange(PublisherPrefixs.GetAllDbPublisherPrefixes());
    }

    private void btnStart_Click(object sender, EventArgs eventarg)
    {
        //var keywords = "blockchain,merkle,bitcoin,ethereum,hyperledger,monero,eosio,algorand,zcash,filecoin,immutable";
        //var keywords = "sgx,trusted execution,privacy,federat,enclave,trustzone,amd sev";
        //var keywords = "zero-knowledge,zero knowledge,authenticated,authentication,authenticating,integrity,verifiable";

        string[] learningKeywords = new[]
        {
            "learning",
            "training",
            "aggregation",
        };

        string[] decorativeKeywords = new[]
        {
            "federated",
            "distributed",
            "collaborative",
            "split",
            "edge",
            "swarm",
            "hierarchical",
            "cross-silo",
            "cross-device",
            "decentralized",
            "peer-to-peer",
            "multi-party",
            "privacy-preserving",
            "secure",
            "cross-organizational",
            "consensus",
        };


        string[] web3Keywords = new[]
        {
            "blockchain",
            "web3",
            "web 3.0",
            "smart contract",
            "ethereum",
            "bitcoin",
            //"decentralized",
            //"token",
            "on-chain",
            "onchain",
            "ipfs",
            "ledger",
            //"ownership",
            //"incentive",
            //"zero-knowledge",
            //"zk"
        };

        var wordsGroups = new string[][]
        {
            learningKeywords,
            //decorativeKeywords,
            web3Keywords,
        };

        var keywords = "federated";
        //var yearstart = 2018;
        var words = keywords
            .Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)
            .Select(_ => _.Trim())
            .ToArray();
        var wordStats = new Dictionary<string, int>();

        var rs = DblpParser.GetRecords(this.txtDBLPfile.Text);
        var yearstart = 2013;
        var yearend = 2026;

        var clses = PublisherPrefixs.GetAllDbPublisherPrefixes();

        var fp = new List<ExportPaper>();
        var fw = new List<ExportPaper>();

        var tasks = rs.UpdateProgress(this.UpdateProgress)
            .MatchKeyPrefix(clses, (yearstart, yearend), _ => fp.Add(new ExportPaper(_)))
            //.MatchWords(words, yearstart, wordStats, _ => fw.Add(new ExportPaper(_)))
            .MatchByKeywordGroups(yearstart, wordsGroups, wordStats, _ => fw.Add(new ExportPaper(_)))
            ;

        foreach (var task in tasks) { }

        var lz4Options = MessagePackSerializerOptions.Standard.WithCompression(MessagePackCompression.Lz4BlockArray);
        File.WriteAllBytes(@"..\..\words.bin", MessagePackSerializer.Serialize(fw, lz4Options));
        File.WriteAllBytes(@"..\..\pub.bin", MessagePackSerializer.Serialize(fp, lz4Options));
        var json = JsonConvert.SerializeObject(
            new { stats = wordStats, filename = Path.GetFileName(this.txtDBLPfile.Text) },
            Newtonsoft.Json.Formatting.Indented,
            new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        File.WriteAllText(@"..\..\stats.json", json);


        //var json = JsonConvert.SerializeObject(
        //    new { records = fw, stats = wordStats, filename = Path.GetFileName(this.txtDBLPfile.Text) },
        //    Newtonsoft.Json.Formatting.Indented,
        //    new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        //File.WriteAllText(@"..\..\papers.js", "var papers = " + json);
    }

    private void UpdateProgress(int totalProcessed, bool isFinished = false)
    {
        if (!isFinished && totalProcessed % 10000 != 0) return;

        Application.DoEvents();
        var i = totalProcessed;
        var progTick = 100_0000;

        this.Invoke((Action)(() =>
        {
            if (isFinished)
            {
                this.lblStatus.Text = $"Finished [{i}]";
                this.barProgress.Maximum = 100;
                this.barProgress.Value = 100;
            }
            else
            {
                this.lblStatus.Text = $"Processing {i} items";
                var m = (i / progTick) + 1;
                m = Math.Max(m, 12);
                this.barProgress.Maximum = m * progTick;
                this.barProgress.Value = i;
            }
        }));
    }

    private void btnIndex_Click(object sender, EventArgs e)
    {
        this.btnIndex.Enabled = false;
        Indexer.ProduceIndex();
        this.btnIndex.Enabled = true;
    }

    private void btnExportDb_Click(object sender, EventArgs e)
    {
        this.btnExportDb.Enabled = false;
        Exporter.ProduceDb();
        this.btnExportDb.Enabled = true;
    }

    private void btnExportPapers_Click(object sender, EventArgs e)
    {
        this.btnExportPapers.Enabled = false;
        Exporter.Export(this.cmbKeyPrefix.SelectedItem as string, this.numYear.Value.ToString(), this.numVolume.Value.ToString());
        this.btnExportPapers.Enabled = true;
    }

    private void btnExportSite_Click(object sender, EventArgs e)
    {
        this.btnExportSite.Enabled = false;
        Exporter.ExportSurveySiteFormat();
        this.btnExportSite.Enabled = true;
    }
}