namespace ExtractDBLPForm;

using MessagePack;
using System;
using System.IO;

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
}
