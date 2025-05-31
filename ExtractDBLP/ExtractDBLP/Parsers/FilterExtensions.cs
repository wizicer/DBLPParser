namespace ExtractDBLPForm.Parsers;

using ExtractDBLPForm.Models;
using System;
using System.Collections.Generic;
using System.Linq;

public static class FilterExtensions
{
    public static IEnumerable<DblpRecord> MatchKeyPrefix(
        this IEnumerable<DblpRecord> records,
        string[] keyPrefixes,
        (int start, int end) year,
        Action<DblpRecord> found)
    {
        foreach (var record in records)
        {
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
                found(record);
            }

            yield return record;
        }
    }

    public static IEnumerable<DblpRecord> MatchWords(
        this IEnumerable<DblpRecord> records,
        string[] words,
        int yearstart,
        Dictionary<string, int> wordStats,
        Action<DblpRecord> found)
    {
        foreach (var record in records)
        {
            var isMatch = false;
            if (!string.IsNullOrEmpty(record.title))
            {
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
            }

            if (isMatch)
            {
                found(record);
            }

            yield return record;
        }
    }

    public static IEnumerable<DblpRecord> MatchFederatedLearning(
        this IEnumerable<DblpRecord> records,
        int yearstart,
        Action<DblpRecord> found)
    {
        foreach (var record in records)
        {
            var isMatch = false;
            if (!string.IsNullOrEmpty(record.title))
            {
                if (record.title.IndexOf("federated", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    if (record.title.IndexOf("learning", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        isMatch = true;

                        // filter year
                        if (isMatch && (record is Paper pp && (!int.TryParse(pp.year, out var y) || y < yearstart)))
                        {
                            isMatch = false;
                        }
                    }
                }
            }

            if (isMatch)
            {
                found(record);
            }

            yield return record;
        }
    }

    public static IEnumerable<DblpRecord> UpdateProgress(
        this IEnumerable<DblpRecord> records,
        Action<int, bool> updateProgress)
    {
        var i = 0;
        foreach (var record in records)
        {
            i++;
            updateProgress(i, false);
            yield return record;
        }

        updateProgress(i, true);
    }
}