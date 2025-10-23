using System;
using System.Threading.Tasks;
using Enterprise.DTO;

namespace Enterprise.Service.Contracts
{
    public interface IMyService
    {
        Task<int> GetZero();

        Task<MyInfo> GetMyInfo();
    }
}
