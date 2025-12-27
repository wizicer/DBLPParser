namespace DblpCli.Helpers;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using DblpCli.Models;

public class PublisherPrefixesLoader
{
    public static string[] LoadFromFile(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"Publisher prefixes file not found: {filePath}");
        }

        var json = File.ReadAllText(filePath);
        var prefixes = JsonConvert.DeserializeObject<string[]>(json);
        
        if (prefixes == null || prefixes.Length == 0)
        {
            throw new InvalidOperationException("Publisher prefixes file is empty or invalid");
        }

        return prefixes;
    }

    public static void SaveToFile(string filePath, string[] prefixes)
    {
        var json = JsonConvert.SerializeObject(prefixes, Formatting.Indented);
        File.WriteAllText(filePath, json);
    }

    public static void SaveDefaultToFile(string filePath)
    {
        var prefixes = PublisherPrefixs.GetAllDbPublisherPrefixes();
        SaveToFile(filePath, prefixes);
    }
}
