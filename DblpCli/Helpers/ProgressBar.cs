namespace DblpCli.Helpers;

using System;

public class ProgressBar
{
    private int _lastReportedCount = 0;
    private readonly int _reportInterval;

    public ProgressBar(int reportInterval = 100000)
    {
        _reportInterval = reportInterval;
    }

    public void Update(int currentCount, bool isFinished)
    {
        if (isFinished)
        {
            Console.WriteLine($"\rFinished processing {currentCount} records.          ");
        }
        else if (currentCount - _lastReportedCount >= _reportInterval)
        {
            Console.Write($"\rProcessing {currentCount} records...");
            _lastReportedCount = currentCount;
        }
    }

    public void Reset()
    {
        _lastReportedCount = 0;
    }
}
