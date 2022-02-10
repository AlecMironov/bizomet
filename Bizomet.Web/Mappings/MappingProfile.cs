using AutoMapper;
using Bizomet.Data.DataEncryption.Attributes;
using Bizomet.Data.DataEncryption.Providers;
using Bizomet.Data.Entities;
using Bizomet.Models;

namespace Bizomet.Web.Mappings
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			var key = Convert.FromBase64String("ztcJaQV8JYok1HQ9GJUvMg==");
			var iv = Convert.FromBase64String("R+cE+5EhP58cgS5UouchvQ==");
			var aesProvider = new AesProvider(key, iv);

			CreateMap<UserRegistrationModel, ApplicationUser>()
				.ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email))
				.ForMember(u => u.UserProfile, opt => opt.Ignore())
				.AfterMap((src, dest, context) =>
				{
					dest.UserProfile = new UserProfile();
					var properties = typeof(UserProfile).GetProperties().Where(p => Attribute.IsDefined(p, typeof(EncryptedAttribute)));
					foreach (var prop in properties) {
						var srcProp = src.GetType().GetProperty(prop.Name);
						if (srcProp != null)
							prop.SetValue(dest.UserProfile, aesProvider.Encrypt(Convert.ToString(srcProp.GetValue(src))));
					}
				});

			CreateMap<UserProfile, UserProfileModel>()
				.AfterMap((src, dest, context) =>
				{
					dest.UserName = src.User.UserName;
					dest.Email = src.User.Email;
					dest.PhoneNumber = src.User.PhoneNumber;
					var properties = src.GetType().GetProperties().Where(p => Attribute.IsDefined(p, typeof(EncryptedAttribute)));
					foreach (var prop in properties) {
						var destProp = dest.GetType().GetProperty(prop.Name);
						if (destProp != null)
							destProp.SetValue(dest, aesProvider.Decrypt(Convert.ToString(prop.GetValue(src))));
					}
				});
		}
	}
}
