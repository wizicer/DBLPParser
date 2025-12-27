namespace DblpCli.Models;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

public class CcfRecord
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("fullname")]
    public string FullName { get; set; }

    [JsonProperty("publisher")]
    public string Publisher { get; set; }

    [JsonProperty("url")]
    public string Url { get; set; }

    [JsonProperty("rank")]
    [JsonConverter(typeof(StringEnumConverter))]
    public CcfRank Rank { get; set; }

    [JsonProperty("type")]
    [JsonConverter(typeof(StringEnumConverter))]
    public CcfType Type { get; set; }

    [JsonProperty("category")]
    public int Category { get; set; }

    [JsonProperty("crossref")]
    public string CrossRef { get; set; }
}

public enum CcfRank
{
    A,
    B,
    C,
}

public enum CcfType
{
    Journal,
    Meeting,
}
