namespace DblpCli.Exporters;

using DblpCli.Models;
using MessagePack;
using System;
using System.IO;
using System.Linq;

public class Exporter
{
    public static void ProduceDb(string inputPath, string outputPath)
    {
        var lz4Options = MessagePackSerializerOptions.Standard.WithCompression(MessagePackCompression.Lz4BlockArray);
        var papers = MessagePackSerializer.Deserialize<ExportPaper[]>(File.ReadAllBytes(inputPath), lz4Options);

        if (File.Exists(outputPath)) File.Delete(outputPath);

        using var db = new LiteDB.LiteDatabase(outputPath);
        var col = db.GetCollection<ExportPaper>("papers");
        col.InsertBulk(papers);
        col.EnsureIndex(_ => _.key);
        
        Console.WriteLine($"Database created at {outputPath} with {papers.Length} papers");
    }

    public static void Export(string inputPath, string outputDir, string keyPrefix, string year, string volume)
    {
        var lz4Options = MessagePackSerializerOptions.Standard.WithCompression(MessagePackCompression.Lz4BlockArray);
        var papers = MessagePackSerializer.Deserialize<ExportPaper[]>(File.ReadAllBytes(inputPath), lz4Options);
        var jsonPath = volume == "0"
            ? Path.Combine(outputDir, $"{keyPrefix.Replace("/", "")}{year}.json")
            : Path.Combine(outputDir, $"{keyPrefix.Replace("/", "")}{volume}.json");
        papers = volume == "0"
            ? papers.Where(_ => _.key.StartsWith(keyPrefix) && _.year == year).ToArray()
            : papers.Where(_ => _.key.StartsWith(keyPrefix) && _.volume == volume).ToArray();
        var json = MessagePackSerializer.SerializeToJson(papers);
        File.WriteAllText(jsonPath, json);
        
        Console.WriteLine($"Exported {papers.Length} papers to {jsonPath}");
    }

    public static void ExportSurveySiteFormat(string inputPath, string outputPath)
    {
        var lz4Options = MessagePackSerializerOptions.Standard.WithCompression(MessagePackCompression.Lz4BlockArray);
        var papers = MessagePackSerializer.Deserialize<ExportPaper[]>(File.ReadAllBytes(inputPath), lz4Options);

        if (File.Exists(outputPath)) File.Delete(outputPath);

        File.WriteAllBytes(outputPath, MessagePackSerializer.Serialize(papers));
        
        Console.WriteLine($"Exported {papers.Length} papers to {outputPath} in survey site format");
    }
}
