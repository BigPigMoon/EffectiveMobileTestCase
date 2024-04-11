using System.Net;

namespace TestCaseEffectiveMobile
{
    internal class Parameters
    {
        public string FileLog { get; set; }
        public string FileOutput { get; set; }
        public IPAddress AddressStart { get; set; }
        public IPAddress AddressMask { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }
    }
}
