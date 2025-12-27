DBLPParser
==========

A CLI tool for parsing and filtering DBLP XML data with configurable rules.

## Quick Start

```bash
# Parse DBLP XML with default rules
dotnet run --project DblpCli -- parse --xml dblp.xml --rules parse-rules.json --output ./output

# Create Lucene index from parsed data
dotnet run --project DblpCli -- index --input output/words.bin --output output/index

# Export to LiteDB database
dotnet run --project DblpCli -- export-db --input output/words.bin --output output/data.db

# Export specific papers to JSON
dotnet run --project DblpCli -- export-papers --input output/pub.bin --output-dir output --key-prefix "journals/pvldb/" --year 2024
```

## Commands

- **parse** - Parse DBLP XML and extract papers using filter rules
- **index** - Create Lucene search index from parsed papers
- **export-db** - Export papers to LiteDB database
- **export-papers** - Export filtered papers to JSON

## Filter Rules File

The `parse-rules.json` file defines filtering rules. Each rule can match papers by publisher prefix or keyword groups.

### Example Configuration

```json
{
  "rules": [
    {
      "name": "publisher",
      "output": "pub.bin",
      "type": "PublisherPrefix",
      "start": 2013,
      "end": 2026,
      "enabled": true,
      "format": "Bin",
      "prefixes": ["journals/pvldb/", "conf/sigmod/"]
    },
    {
      "name": "keywords",
      "output": "words.bin",
      "type": "KeywordGroups",
      "start": 2013,
      "enabled": true,
      "format": "Bin",
      "keywords": [
        ["machine learning", "deep learning"],
        ["privacy", "security"]
      ]
    }
  ]
}
```

### Rule Fields

- **name** - Rule identifier
- **output** - Output file name
- **type** - `PublisherPrefix` or `KeywordGroups`
- **start** - Start year for filtering
- **end** - End year (optional, for PublisherPrefix only)
- **enabled** - Set to `false` to disable a rule
- **format** - Output format: `Raw` (compressed MessagePack), `Json` (text), or `Bin` (uncompressed MessagePack)
- **prefixes** - Publisher key prefixes (for PublisherPrefix type)
- **keywords** - Keyword groups (for KeywordGroups type, all groups must match)

### Output Formats

- **Raw** (default) - MessagePack with LZ4 compression, smallest file size
- **Json** - Human-readable JSON text format
- **Bin** - MessagePack without compression

### Output Files

Each enabled rule generates:
- `{output}` - Matched papers in specified format
- `{name}_stats.json` - Statistics (match count, keyword frequencies, format used)
