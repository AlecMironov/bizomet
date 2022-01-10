//using AutoMapper;
//using Bizomet.Data.Models;
//using Bizomet.Models;

//namespace Bizomet.Web.Mapping
//{
//	public class MappingProfile : Profile
//	{
//		public MappingProfile()
//		{
//			CreateMap<Company, CompanyModel>()
//				.ForMember(c => c.FullAddress, opt => opt.MapFrom(x => string.Join(' ', x.Address, x.Country)));

//			CreateMap<UserRegistrationModel, ApplicationUser>()
//				.ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email));
//		}
//	}
//}
