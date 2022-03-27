using AutoMapper;
using Bizomet.Core.Helpers;

namespace Bizomet.Web.Mappings
{
	public class EnumToStringTypeConverter<T> : ITypeConverter<T, string>
		where T : Enum
	{
		public string Convert(T source, string destination, ResolutionContext context)
		{
			return EnumHelper.GetEnumName(source);
		}
	}
}
