using System.Net;
using TestCaseEffectiveMobile.Extensions;

namespace TestCaseEffectiveMobile
{
    internal class Filter
    {
        public static List<LogEntry> FilterByTime(List<LogEntry> entries, DateTime startTime, DateTime endTime)
        {
            return entries.Where(entry => entry.Timestamp >= startTime && entry.Timestamp <= endTime.AddHours(23).AddMinutes(59)).ToList();
        }

        public static List<LogEntry> FilterByAddress(List<LogEntry> entries, IPAddress? startAddress, IPAddress? endAddress)
        {
            List<LogEntry> filteredEntries = [];

            if (startAddress == null || endAddress == null)
                return entries;

            foreach (var entry in entries)
            {
                if (entry.IPAddress.IsInRange(startAddress, endAddress))
                {
                    filteredEntries.Add(entry);
                }
            }

            return filteredEntries;
        }
    }
}
