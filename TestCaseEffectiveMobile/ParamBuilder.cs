using System.Net;
using TestCaseEffectiveMobile.Interfaces;

namespace TestCaseEffectiveMobile
{
    internal class ParamBuilder : IParamBuilder
    {
        private Parameters? _product;

        public ParamBuilder() => Reset();

        public void AddAddressMask(string mask)
        {
            try
            {
                _product.AddressMask = IPAddress.Parse(mask);
            }
            catch 
            { 
                Console.WriteLine("IP address of mask incorrect!");
                _product.AddressMask = null;
            }
        }

        public void AddAddressStart(string addres)
        {
            try
            {
                _product.AddressStart = IPAddress.Parse(addres);
            } 
            catch
            {
                Console.WriteLine("Start IP address incorrect!");
                _product.AddressStart = null;
            }
        }

        public void AddEndTime(DateTime endTime) => _product.TimeEnd = endTime;

        public void AddFileLogPath(string path) => _product.FileLog = path;

        public void AddFileOutPath(string path) => _product.FileOutput = path;

        public void AddStartTime(DateTime startTime) => _product.TimeStart = startTime;

        public Parameters GetResult()
        {
            var result = _product;

            Reset();

            if (result == null)
                throw new Exception("Result is null.");

            if (result.FileLog == null || result.FileOutput == null || result.TimeStart == null || result.TimeEnd == null)
                throw new Exception("Some required parameters are not set.");

            if (result.AddressMask != null && result.AddressStart == null)
                throw new Exception("Please, set address too.");

            return result;
        }

        public void Reset()
        {
            _product = new Parameters();
        }
    }
}
