using System.CommandLine;
using DblpCli.Models;
using DblpCli.Parsers;
using DblpCli.Exporters;
using MessagePack;
using Newtonsoft.Json;

var rootCommand = new RootCommand("DBLP Parser CLI - Parse and export DBLP XML data");

// ============ PARSE COMMAND ============
var parseCommand = new Command("parse", "Parse DBLP XML file and extract papers");
var xmlFileOption = new Option<string>("--xml", "Path to DBLP XML file") { IsRequired = true };
var outputDirOption = new Option<string>("--output", () => ".", "Output directory for generated files");
var keywordsOption = new Option<string>("--keywords", () => "federated", "Comma-separated keywords to match");
var yearStartOption = new Option<int>("--year-start", () => 2013, "Start year for filtering");
var yearEndOption = new Option<int>("--year-end", () => 2026, "End year for filtering");
var keywordGroupsOption = new Option<string[]>("--keyword-groups", "Keyword groups (each group is comma-separated, use multiple --keyword-groups for AND logic)") { AllowMultipleArgumentsPerToken = true };

parseCommand.AddOption(xmlFileOption);
parseCommand.AddOption(outputDirOption);
parseCommand.AddOption(keywordsOption);
parseCommand.AddOption(yearStartOption);
parseCommand.AddOption(yearEndOption);
parseCommand.AddOption(keywordGroupsOption);

parseCommand.SetHandler((xmlFile, outputDir, keywords, yearStart, yearEnd, keywordGroups) =>
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
    var clses = PublisherPrefixs.GetAllDbPublisherPrefixes();

    var fp = new List<ExportPaper>();
    var fw = new List<ExportPaper>();

    // Parse keyword groups if provided
    string[][] wordsGroups;
    if (keywordGroups != null && keywordGroups.Length > 0)
    {
        wordsGroups = keywordGroups
            .Select(g => g.Split(',').Select(k => k.Trim()).ToArray())
            .ToArray();
    }
    else
    {
        // Default keyword groups
        string[] learningKeywords = new[] { "learning", "training", "aggregation" };
        string[] web3Keywords = new[] { "blockchain", "web3", "web 3.0", "smart contract", "ethereum", "bitcoin", "on-chain", "onchain", "ipfs", "ledger" };
        wordsGroups = new[] { learningKeywords, web3Keywords };
    }

    var lastProgress = 0;
    var tasks = rs.UpdateProgress((count, isFinished) =>
        {
            if (isFinished)
            {
                Console.WriteLine($"\rFinished processing {count} records.          ");
            }
            else if (count - lastProgress >= 100000)
            {
                Console.Write($"\rProcessing {count} records...");
                lastProgress = count;
            }
        })
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

}, xmlFileOption, outputDirOption, keywordsOption, yearStartOption, yearEndOption, keywordGroupsOption);

// ============ INDEX COMMAND ============
var indexCommand = new Command("index", "Create Lucene index from parsed papers");
var indexInputOption = new Option<string>("--input", () => "words.bin", "Input MessagePack file");
var indexOutputOption = new Option<string>("--output", () => "paper_index", "Output index directory");

indexCommand.AddOption(indexInputOption);
indexCommand.AddOption(indexOutputOption);

indexCommand.SetHandler((input, output) =>
{
    Console.WriteLine($"Creating index from {input} to {output}");
    Indexer.ProduceIndex(input, output);
}, indexInputOption, indexOutputOption);

// ============ EXPORT-DB COMMAND ============
var exportDbCommand = new Command("export-db", "Export papers to LiteDB database");
var dbInputOption = new Option<string>("--input", () => "words.bin", "Input MessagePack file");
var dbOutputOption = new Option<string>("--output", () => "data.db", "Output database file");

exportDbCommand.AddOption(dbInputOption);
exportDbCommand.AddOption(dbOutputOption);

exportDbCommand.SetHandler((input, output) =>
{
    Console.WriteLine($"Exporting to database from {input} to {output}");
    Exporter.ProduceDb(input, output);
}, dbInputOption, dbOutputOption);

// ============ EXPORT-PAPERS COMMAND ============
var exportPapersCommand = new Command("export-papers", "Export papers filtered by key prefix, year, or volume");
var papersInputOption = new Option<string>("--input", () => "pub.bin", "Input MessagePack file");
var papersOutputDirOption = new Option<string>("--output-dir", () => ".", "Output directory");
var keyPrefixOption = new Option<string>("--key-prefix", "Key prefix to filter (e.g., journals/pvldb/)") { IsRequired = true };
var filterYearOption = new Option<string>("--year", () => "0", "Year to filter (use 0 to filter by volume instead)");
var filterVolumeOption = new Option<string>("--volume", () => "0", "Volume to filter (use 0 to filter by year instead)");

exportPapersCommand.AddOption(papersInputOption);
exportPapersCommand.AddOption(papersOutputDirOption);
exportPapersCommand.AddOption(keyPrefixOption);
exportPapersCommand.AddOption(filterYearOption);
exportPapersCommand.AddOption(filterVolumeOption);

exportPapersCommand.SetHandler((input, outputDir, keyPrefix, year, volume) =>
{
    Console.WriteLine($"Exporting papers with prefix {keyPrefix}, year={year}, volume={volume}");
    Exporter.Export(input, outputDir, keyPrefix, year, volume);
}, papersInputOption, papersOutputDirOption, keyPrefixOption, filterYearOption, filterVolumeOption);

// ============ EXPORT-SITE COMMAND ============
var exportSiteCommand = new Command("export-site", "Export papers in survey site format");
var siteInputOption = new Option<string>("--input", () => "words.bin", "Input MessagePack file");
var siteOutputOption = new Option<string>("--output", () => "paper.bin", "Output file");

exportSiteCommand.AddOption(siteInputOption);
exportSiteCommand.AddOption(siteOutputOption);

exportSiteCommand.SetHandler((input, output) =>
{
    Console.WriteLine($"Exporting to survey site format from {input} to {output}");
    Exporter.ExportSurveySiteFormat(input, output);
}, siteInputOption, siteOutputOption);

// ============ LIST-PREFIXES COMMAND ============
var listPrefixesCommand = new Command("list-prefixes", "List all available publisher prefixes");
listPrefixesCommand.SetHandler(() =>
{
    var prefixes = PublisherPrefixs.GetAllDbPublisherPrefixes();
    Console.WriteLine("Available publisher prefixes:");
    foreach (var prefix in prefixes.Distinct().OrderBy(p => p))
    {
        Console.WriteLine($"  {prefix}");
    }
});

// Add all commands to root
rootCommand.AddCommand(parseCommand);
rootCommand.AddCommand(indexCommand);
rootCommand.AddCommand(exportDbCommand);
rootCommand.AddCommand(exportPapersCommand);
rootCommand.AddCommand(exportSiteCommand);
rootCommand.AddCommand(listPrefixesCommand);

return await rootCommand.InvokeAsync(args);
