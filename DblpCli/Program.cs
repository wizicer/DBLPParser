using System.CommandLine;
using DblpCli.Models;
using DblpCli.Parsers;
using DblpCli.Exporters;
using DblpCli.Helpers;

var rootCommand = new RootCommand("DBLP Parser CLI - Parse and export DBLP XML data");

// ============ PARSE COMMAND ============
var parseCommand = new Command("parse", "Parse DBLP XML file and extract papers");
var xmlFileOption = new Option<string>("--xml", () => @"D:\LargeWork\dblp\origin_data\dblp.xml", "Path to DBLP XML file") { IsRequired = true };
var rulesFileOption = new Option<string>("--rules", () => "parse-rules.json", "Path to filter rules JSON file") { IsRequired = true };
var outputDirOption = new Option<string>("--output", () => "./target", "Output directory for generated files");

parseCommand.AddOption(xmlFileOption);
parseCommand.AddOption(rulesFileOption);
parseCommand.AddOption(outputDirOption);

parseCommand.SetHandler((xmlFile, rulesFile, outputDir) =>
{
    DblpParserExporter.Execute(xmlFile, rulesFile, outputDir);
}, xmlFileOption, rulesFileOption, outputDirOption);

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

// Add all commands to root
rootCommand.AddCommand(parseCommand);
rootCommand.AddCommand(indexCommand);
rootCommand.AddCommand(exportDbCommand);
rootCommand.AddCommand(exportPapersCommand);
rootCommand.AddCommand(exportSiteCommand);

return await rootCommand.InvokeAsync(args);
