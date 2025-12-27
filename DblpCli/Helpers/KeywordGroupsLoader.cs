namespace DblpCli.Helpers;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

public class KeywordGroupsLoader
{
    public static string[][] LoadFromFile(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"Keyword groups file not found: {filePath}");
        }

        var json = File.ReadAllText(filePath);
        var groups = JsonConvert.DeserializeObject<string[][]>(json);
        
        if (groups == null || groups.Length == 0)
        {
            throw new InvalidOperationException("Keyword groups file is empty or invalid");
        }

        return groups;
    }

    public static string[][] ParseFromCommandLine(string[] keywordGroups)
    {
        if (keywordGroups == null || keywordGroups.Length == 0)
        {
            return null;
        }

        return keywordGroups
            .Select(g => g.Split(',').Select(k => k.Trim()).ToArray())
            .ToArray();
    }

    public static void SaveToFile(string filePath, string[][] keywordGroups)
    {
        var json = JsonConvert.SerializeObject(keywordGroups, Formatting.Indented);
        File.WriteAllText(filePath, json);
    }

    public static string[][] GetDefaultKeywordGroups()
    {
        return new[]
        {
            new[] { "learning", "training", "aggregation" },
            new[] { "blockchain", "web3", "web 3.0", "smart contract", "ethereum", "bitcoin", "on-chain", "onchain", "ipfs", "ledger" }
        };
    }
}
