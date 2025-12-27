namespace DblpCli.Models;

using Newtonsoft.Json;
using System.Collections.Generic;

public class FilterConfig
{
    public List<FilterRule> Rules { get; set; } = new List<FilterRule>();
}

public class FilterRule
{
    public string Name { get; set; } = "";
    
    [JsonProperty("output")]
    public string OutputFile { get; set; } = "";
    
    public FilterType Type { get; set; }
    
    [JsonProperty("start")]
    public int YearStart { get; set; }
    
    [JsonProperty("end")]
    public int? YearEnd { get; set; }
    
    [JsonProperty("prefixes")]
    public string[]? PublisherPrefixes { get; set; }
    
    [JsonProperty("keywords")]
    public string[][]? KeywordGroups { get; set; }
    
    public bool Enabled { get; set; } = true;
    
    public OutputFormat Format { get; set; } = OutputFormat.Raw;
}

public enum FilterType
{
    PublisherPrefix,
    KeywordGroups
}

public enum OutputFormat
{
    Raw,
    Json,
    Bin
}
