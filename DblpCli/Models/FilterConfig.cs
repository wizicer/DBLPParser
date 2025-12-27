namespace DblpCli.Models;

using System.Collections.Generic;

public class FilterConfig
{
    public List<FilterRule> Rules { get; set; } = new List<FilterRule>();
}

public class FilterRule
{
    public string Name { get; set; } = "";
    public string OutputFile { get; set; } = "";
    public FilterType Type { get; set; }
    public int YearStart { get; set; }
    public int? YearEnd { get; set; }
    public string[]? PublisherPrefixes { get; set; }
    public string[][]? KeywordGroups { get; set; }
}

public enum FilterType
{
    PublisherPrefix,
    KeywordGroups
}
