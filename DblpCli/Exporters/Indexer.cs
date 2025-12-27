namespace DblpCli.Exporters;

using DblpCli.Models;
using Lucene.Net.Analysis.En;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Store;
using Lucene.Net.Util;
using MessagePack;
using System;
using System.IO;

public class Indexer
{
    private const LuceneVersion luceneVersion = LuceneVersion.LUCENE_48;

    public static void ProduceIndex(string inputPath, string indexPath)
    {
        var lz4Options = MessagePackSerializerOptions.Standard.WithCompression(MessagePackCompression.Lz4BlockArray);
        var papers = MessagePackSerializer.Deserialize<ExportPaper[]>(File.ReadAllBytes(inputPath), lz4Options);

        var indexConfig = new IndexWriterConfig(luceneVersion, new EnglishAnalyzer(luceneVersion))
        {
            OpenMode = OpenMode.CREATE
        };
        using var indexDir = FSDirectory.Open(indexPath);
        using var writer = new IndexWriter(indexDir, indexConfig);

        foreach (var paper in papers)
        {
            var doc = new Document();
            doc.Add(new StringField(nameof(paper.key), paper.key, Field.Store.YES));
            doc.Add(new StringField(nameof(paper.type), paper.type ?? string.Empty, Field.Store.YES));
            doc.Add(new TextField(nameof(paper.title), paper.title ?? string.Empty, Field.Store.YES));
            doc.Add(new TextField(nameof(paper.year), paper.year ?? string.Empty, Field.Store.YES));
            doc.Add(new TextField(nameof(paper.publisher), paper.publisher ?? string.Empty, Field.Store.YES));
            writer.AddDocument(doc);
        }

        writer.Commit();
        
        Console.WriteLine($"Index created at {indexPath} with {papers.Length} papers");
    }
}
