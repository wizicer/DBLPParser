namespace DblpCli.Exporters;

using DblpCli.Helpers;
using DblpCli.Models;
using DblpCli.Parsers;
using MessagePack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class DblpParserExporter
{
    public static void Execute(
        string xmlFile,
        string outputDir,
        string keywords,
        int yearStart,
        int yearEnd,
        string[] keywordGroupsArgs,
        string keywordGroupsFile,
        string publisherPrefixesFile)
    {
        Console.WriteLine($"Parsing DBLP XML: {xmlFile}");
        Console.WriteLine($"Output directory: {outputDir}");
        Console.WriteLine($"Year range: {yearStart}-{yearEnd}");

        Directory.CreateDirectory(outputDir);

        var words = keywords
            .Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)
            .Select(_ => _.Trim())
            .ToArray();

        var wordStats = new Dictionary<string, int>();
        foreach (var word in words)
        {
            wordStats[word] = 0;
        }

        var rs = DblpParser.GetRecords(xmlFile);
        
        // Load publisher prefixes
        string[] clses;
        if (!string.IsNullOrEmpty(publisherPrefixesFile) && File.Exists(publisherPrefixesFile))
        {
            clses = PublisherPrefixesLoader.LoadFromFile(publisherPrefixesFile);
            Console.WriteLine($"Loaded {clses.Length} publisher prefixes from {publisherPrefixesFile}");
        }
        else
        {
            clses = PublisherPrefixs.GetAllDbPublisherPrefixes();
        }

        var fp = new List<ExportPaper>();
        var fw = new List<ExportPaper>();

        // Parse keyword groups
        string[][] wordsGroups;
        if (!string.IsNullOrEmpty(keywordGroupsFile) && File.Exists(keywordGroupsFile))
        {
            wordsGroups = KeywordGroupsLoader.LoadFromFile(keywordGroupsFile);
            Console.WriteLine($"Loaded keyword groups from {keywordGroupsFile}");
        }
        else if (keywordGroupsArgs != null && keywordGroupsArgs.Length > 0)
        {
            wordsGroups = KeywordGroupsLoader.ParseFromCommandLine(keywordGroupsArgs);
        }
        else
        {
            wordsGroups = KeywordGroupsLoader.GetDefaultKeywordGroups();
        }

        var progressBar = new ProgressBar();
        var tasks = rs.UpdateProgress((count, isFinished) => progressBar.Update(count, isFinished))
            .MatchKeyPrefix(clses, (yearStart, yearEnd), _ => fp.Add(new ExportPaper(_)))
            .MatchByKeywordGroups(yearStart, wordsGroups, wordStats, _ => fw.Add(new ExportPaper(_)));

        foreach (var task in tasks) { }

        var lz4Options = MessagePackSerializerOptions.Standard.WithCompression(MessagePackCompression.Lz4BlockArray);
        var wordsPath = Path.Combine(outputDir, "words.bin");
        var pubPath = Path.Combine(outputDir, "pub.bin");
        var statsPath = Path.Combine(outputDir, "stats.json");

        File.WriteAllBytes(wordsPath, MessagePackSerializer.Serialize(fw, lz4Options));
        File.WriteAllBytes(pubPath, MessagePackSerializer.Serialize(fp, lz4Options));

        var json = JsonConvert.SerializeObject(
            new { stats = wordStats, filename = Path.GetFileName(xmlFile) },
            Formatting.Indented,
            new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        File.WriteAllText(statsPath, json);

        Console.WriteLine($"Saved {fw.Count} keyword-matched papers to {wordsPath}");
        Console.WriteLine($"Saved {fp.Count} publisher-matched papers to {pubPath}");
        Console.WriteLine($"Saved stats to {statsPath}");
    }
}
