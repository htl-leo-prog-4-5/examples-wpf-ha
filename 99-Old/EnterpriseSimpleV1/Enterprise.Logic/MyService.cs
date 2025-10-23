using Enterprise.Logic.Contracts;

using System;
using System.Threading.Tasks;

namespace Enterprise.Logic
{
    public class MyService : IMyService, IDisposable
    {
        public void Dispose()
        {
        }

        public async Task<int> GetZero()
        {
            return await Task.FromResult(0);
        }
    }
}