using OMNI.Data.Services.OMNIAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMNI.Domain.OMNI.TestRepo.Impl
{
    public class TestRepoImpl : ITestRepo
    {
        private readonly TestService _test;

        public TestRepoImpl(TestService test)
        {
            _test = test;
        }

        public async Task<List<string>> GetPortName()
            => await _test.GetPortName();

        public async Task<List<string>> GetPortNameStartWith(char huruf)
        {
            var result = await _test.GetPortName();
            return result.Where(b=>b.StartsWith(huruf)).ToList();
        }
    }
}
