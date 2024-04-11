using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCaseEffectiveMobile.Interfaces
{
    internal interface IParamBuilder
    {
        public void Reset();
        public void AddFileLogPath(string path);
        public void AddFileOutPath(string path);
        public void AddStartTime(DateTime startTime);
        public void AddEndTime(DateTime endTime);
        public void AddAddressStart(string addres);
        public void AddAddressMask(string mask);
    }
}
