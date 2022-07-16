using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OMNI.Domain.OMNI.TestRepo
{
    public interface ITestRepo
    {
        public Task<List<string>> GetPortName();
        public Task<List<string>> GetPortNameStartWith(char huruf);
    }
}
