using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EnterpriseSimpleV2.Logic.Abstraction.DTOs;

namespace EnterpriseSimpleV2.Logic.Abstraction
{
    public interface IMyTableManager
    {
        Task<IEnumerable<MyTable>> GetAll();
        Task<MyTable> Get(int id);
        Task<int> Add(MyTable value);
    }
}