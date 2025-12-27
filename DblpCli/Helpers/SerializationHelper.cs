namespace DblpCli.Helpers;

using DblpCli.Models;
using MessagePack;
using System.IO;

public static class SerializationHelper
{
    public static void SerializePapers(ExportPaper[] papers, string outputPath, OutputFormat format)
    {
        switch (format)
        {
            case OutputFormat.Raw:
                SerializeRaw(papers, outputPath);
                break;
            case OutputFormat.Json:
                SerializeJson(papers, outputPath);
                break;
            case OutputFormat.Bin:
                SerializeBin(papers, outputPath);
                break;
        }
    }

    private static void SerializeRaw(ExportPaper[] papers, string outputPath)
    {
        var lz4Options = MessagePackSerializerOptions.Standard.WithCompression(MessagePackCompression.Lz4BlockArray);
        File.WriteAllBytes(outputPath, MessagePackSerializer.Serialize(papers, lz4Options));
    }

    private static void SerializeJson(ExportPaper[] papers, string outputPath)
    {
        var json = MessagePackSerializer.SerializeToJson(papers);
        File.WriteAllText(outputPath, json);
    }

    private static void SerializeBin(ExportPaper[] papers, string outputPath)
    {
        File.WriteAllBytes(outputPath, MessagePackSerializer.Serialize(papers));
    }
}
