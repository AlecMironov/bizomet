using AutoMapper;
using Bizomet.Data.Entities;
using Bizomet.Models;

namespace Bizomet.Web.Mappings
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			//CreateMap<Company, CompanyDto>()
			//		.ForMember(c => c.FullAddress,
			//			opt => opt.MapFrom(x => string.Join(' ', x.Address, x.Country)));

			CreateMap<UserRegistrationModel, ApplicationUser>()
				.ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email));
		}
	}
}
