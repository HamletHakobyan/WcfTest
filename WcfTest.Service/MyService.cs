using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WcfTest.Contracts;
using WcfTest.Contracts.Service;

namespace WcfTest.Service
{
    public class MyService : IMyService
    {
        public async Task<int> GetAgeAsync()
        {
            await Task.Delay(2000);
            return 42;
        }
    }
}
