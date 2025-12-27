namespace DblpCli.Exporters;

using DblpCli.Exporters.Filters;
using DblpCli.Helpers;
using DblpCli.Models;
using DblpCli.Parsers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class DblpParserExporter
{
    public static void Execute(string xmlFile, string rulesFile, string outputDir)
    {
        Console.WriteLine($"Parsing DBLP XML: {xmlFile}");
        Console.WriteLine($"Rules file: {rulesFile}");
        Console.WriteLine($"Output directory: {outputDir}");

        Directory.CreateDirectory(outputDir);

        // Load configuration
        var config = LoadConfig(rulesFile);
        Console.WriteLine($"Loaded {config.Rules.Count} filter rules");

        // Create filters
        var filters = config.Rules.Select(rule => new UnifiedFilter(rule, outputDir)).ToList();

        // Execute single scan with chained FilterExtensions
        var progressBar = new ProgressBar();
        ExecuteScanWithFilters(xmlFile, filters, progressBar);

        // Finish all filters
        foreach (var filter in filters)
        {
            filter.Finish();
            Console.WriteLine($"[{filter.Name}] Saved {filter.MatchCount} papers to {filter.OutputPath}");
            Console.WriteLine($"[{filter.Name}] Saved stats to {filter.StatsPath}");
        }
    }

    private static void ExecuteScanWithFilters(
        string xmlFile,
        List<UnifiedFilter> filters,
        ProgressBar progressBar)
    {
        var records = DblpParser.GetRecords(xmlFile);

        // Chain all filter extensions using existing FilterExtensions methods
        foreach (var filter in filters)
        {
            var rule = filter.GetRule();
            var stats = filter.GetStats();

            switch (rule.Type)
            {
                case FilterType.PublisherPrefix:
                    if (rule.PublisherPrefixes != null && rule.YearEnd.HasValue)
                    {
                        records = records.MatchKeyPrefix(
                            rule.PublisherPrefixes,
                            (rule.YearStart, rule.YearEnd.Value),
                            record => filter.AddMatch(record));
                    }
                    break;

                case FilterType.KeywordGroups:
                    if (rule.KeywordGroups != null)
                    {
                        records = records.MatchByKeywordGroups(
                            rule.YearStart,
                            rule.KeywordGroups,
                            stats,
                            record => filter.AddMatch(record));
                    }
                    break;
            }
        }

        // Add progress tracking
        records = records.UpdateProgress((count, isFinished) => progressBar.Update(count, isFinished));

        // Materialize the stream to execute all filters
        foreach (var _ in records) { }
    }

    private static FilterConfig LoadConfig(string rulesFile)
    {
        if (!File.Exists(rulesFile))
        {
            throw new FileNotFoundException($"Rules file not found: {rulesFile}");
        }

        var json = File.ReadAllText(rulesFile);
        var config = JsonConvert.DeserializeObject<FilterConfig>(json);

        if (config == null || config.Rules == null || config.Rules.Count == 0)
        {
            throw new InvalidOperationException("Rules file is empty or invalid");
        }

        return config;
    }
}
