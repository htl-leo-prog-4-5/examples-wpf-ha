using System;
using System.Threading.Tasks;

namespace Enterprise.Service.Contracts
{
    public interface IMyService
    {
        Task<int> GetZero();
    }
}
