using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Bizomet.Core.Helpers
{
	public static class EnumHelper
	{
		/// <summary>
		/// Get 'DefaultValue' attribute value for given enumeration value.
		/// </summary>
		public static object GetEnumDefaultValue(Enum value)
		{
			if (value == null)
				throw new ArgumentNullException("value");//MLHIDE

			DefaultValueAttribute[] attributes = GetEnumAttributes<DefaultValueAttribute>(value);

			if (attributes.Length > 0)
				return attributes[0].Value;

			return value.ToString();
		}

		/// <summary>
		/// Get 'Display' attribute name value for given enumeration value.
		/// </summary>
		public static string GetEnumName(Enum value)
		{
			if (value == null)
				throw new ArgumentNullException("value");

			DisplayAttribute[] displayAttributes = GetEnumAttributes<DisplayAttribute>(value);

			if (displayAttributes != null && displayAttributes.Length > 0) {
				var resourceManager = new System.Resources.ResourceManager(typeof(Resources.General));
				return displayAttributes[0].ResourceType != null ? resourceManager.GetString(displayAttributes[0].Name) : displayAttributes[0].Name;
			}

			return value.ToString();
		}

		/// <summary>
		/// Get 'Display' or 'Description' attribute name value for given enumeration value.
		/// </summary>
		public static string GetEnumDescription(Enum value)
		{
			if (value == null)
				throw new ArgumentNullException("value");

			DisplayAttribute[] displayAttributes = GetEnumAttributes<DisplayAttribute>(value);

			if (displayAttributes != null && displayAttributes.Length > 0) {
				var resourceManager = new System.Resources.ResourceManager(typeof(Resources.General));
				return displayAttributes[0].ResourceType != null ? resourceManager.GetString(displayAttributes[0].Description) : displayAttributes[0].Description;
			}

			DescriptionAttribute[] attributes = GetEnumAttributes<DescriptionAttribute>(value);

			if (attributes != null && attributes.Length > 0)
				return attributes[0].Description;

			return value.ToString();
		}

		public static Enum GetEnumByDescription(Type enumType, string value)
		{
			var enumValues = Enum.GetValues(enumType);

			foreach (Enum enumValue in enumValues) {
				var enumDescription = GetEnumDescription(enumValue);
				if (string.Compare(enumDescription, value, true) == 0)
					return enumValue;
			}

			return null;
		}

		public static Enum GetEnumByDescription<T>(string value)
		{
			return GetEnumByDescription(typeof(T), value);
		}

		/// <summary>
		/// Convert given string to enumeration value of given type.
		/// </summary>
		public static T ToEnum<T>(string value)
		{
			return (T) Enum.Parse(typeof(T), value, true);
		}

		/// <summary>
		/// Get attributes of given type for given enumeration value.
		/// </summary>
		private static T[] GetEnumAttributes<T>(Enum value)
			where T : class
		{
			Debug.Assert(value != null, "Invalid enumeration value.");//MLHIDE

			var fieldInfo = value.GetType().GetField(value.ToString());

			return fieldInfo.GetCustomAttributes(typeof(T), false) as T[];
		}
	}
}
