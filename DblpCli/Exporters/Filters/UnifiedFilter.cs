namespace DblpCli.Exporters.Filters;

using DblpCli.Models;
using MessagePack;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class UnifiedFilter
{
    private readonly FilterRule _rule;
    private readonly string _outputPath;
    private readonly string _statsPath;
    private readonly List<ExportPaper> _matches;
    private readonly Dictionary<string, int> _stats;

    public int MatchCount => _matches.Count;
    public string OutputPath => _outputPath;
    public string StatsPath => _statsPath;
    public string Name => _rule.Name;

    public UnifiedFilter(FilterRule rule, string outputDir)
    {
        _rule = rule;
        _outputPath = Path.Combine(outputDir, rule.OutputFile);
        _statsPath = Path.Combine(outputDir, $"{rule.Name}_stats.json");
        _matches = new List<ExportPaper>();
        _stats = new Dictionary<string, int>();
    }

    public void AddMatch(DblpRecord record)
    {
        _matches.Add(new ExportPaper(record));
    }

    public Dictionary<string, int> GetStats()
    {
        return _stats;
    }

    public FilterRule GetRule()
    {
        return _rule;
    }

    public void Finish()
    {
        var lz4Options = MessagePackSerializerOptions.Standard.WithCompression(MessagePackCompression.Lz4BlockArray);
        File.WriteAllBytes(_outputPath, MessagePackSerializer.Serialize(_matches.ToArray(), lz4Options));

        var json = JsonConvert.SerializeObject(
            new { stats = _stats, rule = _rule.Name, count = _matches.Count },
            Formatting.Indented,
            new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        File.WriteAllText(_statsPath, json);
    }
}
