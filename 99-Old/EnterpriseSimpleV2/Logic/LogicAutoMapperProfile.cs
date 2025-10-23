using AutoMapper;
using EnterpriseSimpleV2.Repository.Abstraction.Entities;

namespace EnterpriseSimpleV2.Logic
{
    public sealed class LogicAutoMapperProfile : Profile
    {
        public LogicAutoMapperProfile()
        {
            CreateMap<MyTable, EnterpriseSimpleV2.Logic.Abstraction.DTOs.MyTable>().ReverseMap();
        }
    }
}