using System;
using System.Net;
using TestCaseEffectiveMobile;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length < 4)
        {
            Console.WriteLine("Usage: --file-log [path_to_log_file] --file-output [path_to_output_file] --time-start [start_date] --time-end [end_date]");
            return;
        }

        var parameters = MakeParameters(args);
        if (parameters == null) Environment.Exit(1);

        var logEntries = ReadLogEntries(parameters.FileLog);

        var filteredEntries = Filter.FilterByTime(logEntries, parameters.TimeStart, parameters.TimeEnd);
        filteredEntries = Filter.FilterByAddress(filteredEntries, parameters.AddressStart, parameters.AddressMask);

        var addressCounts = CountAddressAccess(filteredEntries);

        WriteResultsToFile(parameters.FileOutput, addressCounts);

        Console.WriteLine("Results have been written to the output file.");
    }

    private static Parameters? MakeParameters(string[] args)
    {
        var paramBuilder = new ParamBuilder();

        for (int i = 0; i < args.Length; i += 2)
        {
            switch (args[i])
            {
                case "--file-log":
                    paramBuilder.AddFileLogPath(args[i + 1]);
                    break;
                case "--file-output":
                    paramBuilder.AddFileOutPath(args[i + 1]);
                    break;
                case "--address-start":
                    paramBuilder.AddAddressStart(args[i + 1]);
                    break;
                case "--address-mask":
                    paramBuilder.AddAddressMask(args[i + 1]);
                    break;
                case "--time-start":
                    paramBuilder.AddStartTime(DateTime.ParseExact(args[i + 1], "dd.MM.yyyy", null));
                    break;
                case "--time-end":
                    paramBuilder.AddEndTime(DateTime.ParseExact(args[i + 1], "dd.MM.yyyy", null));
                    break;
            }
        }

        try
        {
            return paramBuilder.GetResult();
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return null;
        }
    }

    static List<LogEntry> ReadLogEntries(string filePath)
    {
        var logEntries = new List<LogEntry>();

        foreach (var line in File.ReadAllLines(filePath))
        {
            var parts = line.Split([':'], 2);
            if (parts.Length == 2)
            {
                _ = IPAddress.TryParse(parts[0], out var ip);

                if (ip == null) continue;

                var entry = new LogEntry { IPAddress = ip, Timestamp = DateTime.Parse(parts[1]) };
                logEntries.Add(entry);
            }
        }

        return logEntries;
    }

    static Dictionary<IPAddress, int> CountAddressAccess(List<LogEntry> entries)
    {
        var counts = new Dictionary<IPAddress, int>();

        foreach (var entry in entries)
        {
            counts[entry.IPAddress] = counts.TryGetValue(entry.IPAddress, out int value) ? ++value : 1;
        }

        return counts;
    }

    static void WriteResultsToFile(string filePath, Dictionary<IPAddress, int> counts)
    {
        using var writer = new StreamWriter(filePath);
        foreach (var kvp in counts)
        {
            writer.WriteLine($"{kvp.Key}: {kvp.Value}");
        }
    }
}
