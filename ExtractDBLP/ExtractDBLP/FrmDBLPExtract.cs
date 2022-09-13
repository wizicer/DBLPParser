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
        this.txtDBLPfile.Text = @"E:\Works\dblp\dblp.xml";
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

        var clses = PublisherPrefixs.GetAllDbPublisherPrefixes();
        var fw = rs.FilterByKeyPrefix(clses, (yearstart, yearend), this.UpdateProgress)
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
}