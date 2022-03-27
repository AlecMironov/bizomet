using AutoMapper;
using Bizomet.Core.Helpers;

namespace Bizomet.Web.Mappings
{
	public class StringToEnumTypeConverter<T> : ITypeConverter<string, T>
		where T : Enum
	{
		public T Convert(string source, T destination, ResolutionContext context)
		{
			return EnumHelper.ToEnum<T>(source);
		}
	}
}
