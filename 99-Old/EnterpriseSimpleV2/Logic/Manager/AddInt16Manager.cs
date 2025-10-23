using System.Threading.Tasks;
using EnterpriseSimpleV2.Logic.Abstraction;
using EnterpriseSimpleV2.Logic.Abstraction.DTOs;

namespace EnterpriseSimpleV2.Logic.Manager
{
    public class AddInt16Manager : IAddInt16Manager
    {
        public async Task<int> AddSimple(int a, int b)
        {
            return await Task.FromResult(a + b);
        }

        public async Task<Add16> Add(int a, int b)
        {
            return await Task.FromResult(new Add16() {Arg1 = a, Arg2 = b, Result = a + b});
        }
    }
}