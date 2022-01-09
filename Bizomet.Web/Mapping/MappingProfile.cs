using AutoMapper;
using Bizomet.Data.Models;
using Bizomet.ViewModels;

namespace Bizomet.Web.Mapping
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Company, CompanyViewModel>()
				.ForMember(c => c.FullAddress, opt => opt.MapFrom(x => string.Join(' ', x.Address, x.Country)));

			CreateMap<UserForRegistrationViewModel, ApplicationUser>()
				.ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email));
		}
	}
}
