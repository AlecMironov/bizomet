using AutoMapper;
using Bizomet.Core.Enums;
using Bizomet.Core.Helpers;
using Bizomet.Data.DataEncryption.Attributes;
using Bizomet.Data.DataEncryption.Providers;
using Bizomet.Data.Entities;
using Bizomet.Models;

namespace Bizomet.Web.Mappings
{
	public class MappingProfile : Profile
	{
		private readonly AesProvider _aesProvider;
		public MappingProfile()
		{
			CreateMap<string, Guid>().ConvertUsing(s => Guid.Parse(s));
			CreateMap<Guid, string>().ConvertUsing(g => g.ToString("N"));

			var key = Convert.FromBase64String("ztcJaQV8JYok1HQ9GJUvMg==");
			var iv = Convert.FromBase64String("R+cE+5EhP58cgS5UouchvQ==");
			_aesProvider = new AesProvider(key, iv);

			CreateMap<ApplicationUser, AuthResponseModel>()
				.ForMember(u => u.picture, opt => opt.MapFrom(x => x.UserProfile.Picture))
				.AfterMap((src, dest, context) =>
				{
					dest.firstName = _aesProvider.Decrypt(src.UserProfile.FirstName);
					dest.lastName = _aesProvider.Decrypt(src.UserProfile.LastName);
					dest.picture = src.UserProfile.Picture;
					dest.roles = new List<string>(src.UserRoles.Select(r => r.Role.Name).Where(r => r != "Administrator"));
				});

			CreateMap<UserRegistrationModel, ApplicationUser>()
				.ForMember(u => u.UserProfile, opt => opt.Ignore())
				.AfterMap((src, dest, context) =>
				{
					dest.UserProfile = new UserProfile();
					Encrypt(src, dest.UserProfile);
				});

			CreateMap<UserProfile, UserProfileModel>()
				.AfterMap((src, dest, context) =>
				{
					Decrypt(src, dest);
					dest.Roles = new List<string>(src.User.UserRoles.Select(r => r.Role.Name).Where(r => r != "Administrator"));
				})
				.ReverseMap()
				.AfterMap((src, dest, context) => Encrypt(src, dest));

			CreateMap<UserPortfolio, UserPortfolioModel>()
				.AfterMap((src, dest, context) =>
				{
					if (!Uri.IsWellFormedUriString(src.Link, UriKind.Absolute))
						dest.Link = $"http://{src.Link}";
				})
				.ReverseMap()
				.AfterMap((src, dest, context) =>
				{
					if (!Uri.IsWellFormedUriString(src.Link, UriKind.Absolute))
						dest.Link = $"http://{src.Link}";
				});

			CreateMap<ContactUsRequest, ContactUsRequestModel>()
				.ForMember(dest => dest.Reason, opt => opt.MapFrom(src => EnumHelper.GetEnumName(src.Reason)))
				.ReverseMap()
				.ForMember(dest => dest.Reason, opt => opt.MapFrom(src => EnumHelper.ToEnum<ContactReason>(src.Reason)));

			CreateMap<Inquiry, InquiryModel>().ReverseMap();

			CreateMap<Project, ProjectModel>()
				.ForMember(dest => dest.InterviewCondition, opt => opt.MapFrom(src => EnumHelper.GetEnumName(src.InterviewCondition)))
				.ForMember(dest => dest.InterviewResult, opt => opt.MapFrom(src => EnumHelper.GetEnumName(src.InterviewResult)))
				.ForMember(dest => dest.MediaAssistantFinancialService, opt => opt.MapFrom(src => EnumHelper.GetEnumName(src.MediaAssistantFinancialService)))
				.ForMember(dest => dest.ProducerFinancialService, opt => opt.MapFrom(src => EnumHelper.GetEnumName(src.ProducerFinancialService)))
				.ForMember(dest => dest.PromoterFinancialService, opt => opt.MapFrom(src => EnumHelper.GetEnumName(src.PromoterFinancialService)))
				.ReverseMap()
				.ForMember(dest => dest.InterviewCondition, opt => opt.MapFrom(src => EnumHelper.ToEnum<InterviewCondition>(src.InterviewCondition)))
				.ForMember(dest => dest.InterviewResult, opt => opt.MapFrom(src => EnumHelper.ToEnum<InterviewResult>(src.InterviewResult)))
				.ForMember(dest => dest.MediaAssistantFinancialService, opt => opt.MapFrom(src => EnumHelper.ToEnum<FinancialType>(src.MediaAssistantFinancialService)))
				.ForMember(dest => dest.ProducerFinancialService, opt => opt.MapFrom(src => EnumHelper.ToEnum<FinancialType>(src.ProducerFinancialService)))
				.ForMember(dest => dest.PromoterFinancialService, opt => opt.MapFrom(src => EnumHelper.ToEnum<FinancialType>(src.PromoterFinancialService)));

			CreateMap<ProjectAttachment, ProjectAttachmentModel>()
				.ForMember(dest => dest.BinaryContent, opt => opt.MapFrom(src => System.Convert.ToBase64String(src.BinaryContent)))
				.ReverseMap()
				.ForMember(dest => dest.BinaryContent, opt => opt.MapFrom(src => ConvertFromBase64String(src.BinaryContent)));
		}

		private void Decrypt<TSource, TDest>(TSource src, TDest dest)
		{
			if (src == null)
				throw new ArgumentNullException(nameof(src));
			if (dest == null)
				throw new ArgumentNullException(nameof(dest));

			var properties = src.GetType().GetProperties().Where(p => Attribute.IsDefined(p, typeof(EncryptedAttribute)));
			foreach (var prop in properties) {
				var destProp = dest.GetType().GetProperty(prop.Name);
				if (destProp != null) {
					var srcValue = Convert.ToString(prop.GetValue(src));
					if (!string.IsNullOrEmpty(srcValue))
						srcValue = _aesProvider.Decrypt(srcValue);
					destProp.SetValue(dest, srcValue);
				}
			}
		}

		private void Encrypt<TSource, TDest>(TSource src, TDest dest)
		{
			if (src == null)
				throw new ArgumentNullException(nameof(src));
			if (dest == null)
				throw new ArgumentNullException(nameof(dest));

			var properties = dest.GetType().GetProperties().Where(p => Attribute.IsDefined(p, typeof(EncryptedAttribute)));
			foreach (var prop in properties) {
				var srcProp = src.GetType().GetProperty(prop.Name);
				if (srcProp != null) {
					var srcValue = Convert.ToString(srcProp.GetValue(src));
					if (!string.IsNullOrEmpty(srcValue))
						srcValue = _aesProvider.Encrypt(srcValue);
					prop.SetValue(dest, srcValue);
				}
			}
		}

		private byte[]? ConvertFromBase64String(string src)
		{
			if (string.IsNullOrEmpty(src))
				return null;

			if (src.StartsWith("data:")) {
				var parts = src.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
				src = parts[1];
			}

			return System.Convert.FromBase64String(src);
		}
	}
}
