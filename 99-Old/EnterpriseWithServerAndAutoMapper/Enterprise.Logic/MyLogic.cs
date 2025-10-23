using Enterprise.DTO;
using Enterprise.Logic.Contracts;
using System;
using System.Threading.Tasks;

namespace Enterprise.Logic
{
    public class MyLogic : IMyLogic
    {
        public async Task<MyInfo> GetInfo()
        {
            return await Task.FromResult(new MyInfo() { Name = "Maxi" });
        }

        public int GetZero()
        {
            return 0;
        }
    }
}
