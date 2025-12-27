namespace DblpCli.Helpers;

using System;

public class ProgressBar
{
    private int _lastReportedCount = 0;
    private readonly int _reportInterval;
    private readonly bool _isTty;
    private int _lastLineLength = 0;

    public ProgressBar(int reportInterval = 100000)
    {
        _reportInterval = reportInterval;
        _isTty = Console.IsOutputRedirected == false && Console.IsErrorRedirected == false;
    }

    public void Update(int currentCount, bool isFinished)
    {
        if (isFinished)
        {
            if (_isTty)
            {
                ClearLine();
                Console.WriteLine($"Finished processing {currentCount:N0} records.");
            }
            else
            {
                Console.WriteLine($"Finished processing {currentCount:N0} records.");
            }
        }
        else if (currentCount - _lastReportedCount >= _reportInterval)
        {
            var message = $"Processing {currentCount:N0} records...";
            
            if (_isTty)
            {
                ClearLine();
                Console.Write(message);
                _lastLineLength = message.Length;
            }
            else
            {
                Console.WriteLine(message);
            }
            
            _lastReportedCount = currentCount;
        }
    }

    private void ClearLine()
    {
        if (_lastLineLength > 0)
        {
            Console.Write('\r' + new string(' ', _lastLineLength) + '\r');
        }
        else
        {
            Console.Write('\r');
        }
    }

    public void Reset()
    {
        _lastReportedCount = 0;
        _lastLineLength = 0;
    }
}
