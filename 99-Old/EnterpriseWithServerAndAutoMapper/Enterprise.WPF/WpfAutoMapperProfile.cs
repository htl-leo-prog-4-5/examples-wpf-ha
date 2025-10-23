using AutoMapper;

namespace Enterprise.WPF
{
    public sealed class WpfAutoMapperProfile : Profile
	{
		public WpfAutoMapperProfile()
		{
			CreateMap<Models.MyInfo, DTO.MyInfo>().ReverseMap();
		}
	}
}
