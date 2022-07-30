namespace ExtractDBLPForm.Parsers;

using ExtractDBLPForm.Models;
using System;
using System.Collections.Generic;
using System.Linq;

public static class FilterExtensions
{
    public static IEnumerable<DblpRecord> FilterByKeyPrefix(
        this IEnumerable<DblpRecord> records,
        string[] keyPrefixes,
        (int start, int end) year,
        Action<int, int, bool> updateProgress)
    {
        var i = 0;
        var p = 0;
        foreach (var record in records)
        {
            i++;
            var isMatch = false;

            if (keyPrefixes.Any(_ => record.key.StartsWith(_)))
                isMatch = true;

            // filter year
            if (isMatch && (record is Paper pp && (!int.TryParse(pp.year, out var y) || (y < year.start || y > year.end))))
            {
                isMatch = false;
            }

            if (isMatch)
            {
                p++;
                yield return record;
            }

            //if (p > 100) yield break;

            updateProgress(i, p, false);
        }

        updateProgress(i, p, true);
    }
    public static IEnumerable<DblpRecord> FilterByWords(
        this IEnumerable<DblpRecord> records,
        string[] words,
        int yearstart,
        Dictionary<string, int> wordStats,
        Action<int, int, bool> updateProgress)
    {
        var i = 0;
        var p = 0;
        foreach (var record in records)
        {
            i++;
            var isMatch = false;
            // filter words
            foreach (var word in words)
            {
                if (record.title.IndexOf(word, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    isMatch = true;

                    // filter year
                    if (isMatch && (record is Paper pp && (!int.TryParse(pp.year, out var y) || y < yearstart)))
                    {
                        isMatch = false;
                    }

                    if (isMatch) wordStats[word]++;
                }
            }

            if (isMatch)
            {
                p++;
                yield return record;
            }

            updateProgress(i, p, false);
        }

        updateProgress(i, p, true);
    }
}