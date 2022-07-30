namespace ExtractDBLPForm.Exporters;

using ExtractDBLPForm.Models;
using MessagePack;
using System;
using System.IO;
using System.Linq;

public class Exporter
{
    public static void ProduceDb()
    {
        var lz4Options = MessagePackSerializerOptions.Standard.WithCompression(MessagePackCompression.Lz4BlockArray);
        var papers = MessagePackSerializer.Deserialize<ExportPaper[]>(File.ReadAllBytes(@"..\..\data.bin"), lz4Options);

        var dbpath = @"../../data.db";
        if (File.Exists(dbpath)) File.Delete(dbpath);

        using var db = new LiteDB.LiteDatabase(dbpath);
        var col = db.GetCollection<ExportPaper>("papers");
        col.InsertBulk(papers);
        col.EnsureIndex(_ => _.key);
    }

    public static void Export(string keyPrefix, string year)
    {
        var lz4Options = MessagePackSerializerOptions.Standard.WithCompression(MessagePackCompression.Lz4BlockArray);
        var papers = MessagePackSerializer.Deserialize<ExportPaper[]>(File.ReadAllBytes(@"..\..\data.bin"), lz4Options);

        var jsonPath = @$"../../{keyPrefix.Replace("/", "")}{year}.json";
        papers = papers.Where(_ => _.key.StartsWith(keyPrefix) && _.year == year).ToArray();
        var json = MessagePackSerializer.SerializeToJson(papers);
        File.WriteAllText(jsonPath, json);
    }
}
