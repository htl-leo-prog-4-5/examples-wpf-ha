using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EnterpriseSimpleV2.Logic.Abstraction.DTOs;

namespace EnterpriseSimpleV2.Logic.Abstraction
{
    public interface IAddInt16Manager
    {
        Task<int> AddSimple(int a, int b);
        Task<Add16> Add(int a, int b);
    }
}