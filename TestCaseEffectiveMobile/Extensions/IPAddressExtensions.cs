using System.Net;

namespace TestCaseEffectiveMobile.Extensions
{
    internal static class IPAddressExtensions
    {
        public static bool IsInRange(this IPAddress address, IPAddress start, IPAddress? end)
        {
            byte[] addressBytes = address.GetAddressBytes();
            byte[] startBytes = start.GetAddressBytes();
            byte[]? endBytes = null;

            if (end != null)
            {
                endBytes = end.GetAddressBytes();
            }

            bool lowerBound = true;
            bool upperBound = true;

            for (int i = 0; i < addressBytes.Length; i++)
            {
                if (addressBytes[i] < startBytes[i])
                {
                    lowerBound = false;
                }
                if (endBytes != null && addressBytes[i] > endBytes[i])
                {
                    upperBound = false;
                }
            }

            return lowerBound && (endBytes == null || upperBound);
        }
    }
}
