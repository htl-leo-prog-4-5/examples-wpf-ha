using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EnterpriseSimpleV2.Repository.Abstraction.Entities;

namespace EnterpriseSimpleV2.Logic.Abstraction
{
    public interface IMyTableRepository
    {
        Task<IList<MyTable>> GetAll();
        Task<MyTable> Get(int id);
        Task<int> Add(MyTable value);
    }
}