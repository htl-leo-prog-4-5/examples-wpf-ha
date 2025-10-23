using System;
using System.Threading.Tasks;

namespace Enterprise.Logic.Contracts
{
    public interface IMyService
    {
        Task<int> GetZero();
    }
}