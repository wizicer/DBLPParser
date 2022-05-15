namespace ExtractDBLPForm;

using Lucene.Net.Analysis.En;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Store;
using Lucene.Net.Util;
using MessagePack;
using System.IO;

public class Indexer
{
    const LuceneVersion luceneVersion = LuceneVersion.LUCENE_48;

    public static void ProduceIndex()
    {
        var lz4Options = MessagePackSerializerOptions.Standard.WithCompression(MessagePackCompression.Lz4BlockArray);
        var papers = MessagePackSerializer.Deserialize<ExportPaper[]>(File.ReadAllBytes(@"..\..\data.bin"), lz4Options);

        var indexConfig = new IndexWriterConfig(luceneVersion, new EnglishAnalyzer(luceneVersion))
        {
            OpenMode = OpenMode.CREATE
        };
        using var indexDir = FSDirectory.Open("../../paper_index");
        using var writer = new IndexWriter(indexDir, indexConfig);

        foreach (var paper in papers)
        {
            var doc = new Document();
            doc.Add(new StringField(nameof(paper.type), paper.type ?? string.Empty, Field.Store.YES));
            doc.Add(new TextField(nameof(paper.title), paper.title ?? string.Empty, Field.Store.YES));
            doc.Add(new NumericDocValuesField(nameof(paper.year), int.TryParse(paper.year, out var y) ? y : -1));
            doc.Add(new TextField(nameof(paper.publisher), paper.publisher ?? string.Empty, Field.Store.YES));
            writer.AddDocument(doc);
        }

        writer.Commit();
    }
}
