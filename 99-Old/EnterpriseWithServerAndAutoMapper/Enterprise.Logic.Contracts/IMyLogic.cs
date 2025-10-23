using System;
using System.Threading.Tasks;
using Enterprise.DTO;

namespace Enterprise.Logic.Contracts
{
    public interface IMyLogic
    {
        int GetZero();

        Task<MyInfo> GetInfo();
    }
}
