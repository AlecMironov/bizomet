using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace Bizomet.Core.Helpers
{
	/// <summary>
	/// Serialization helper class.
	/// </summary>
	public static class SerializationHelper
	{
		/// <summary>
		/// Deserialize give json string value to given type.
		/// </summary>
		/// <param name="json"></param>
		/// <param name="type"></param>
		/// <returns></returns>
		public static object JsonParse(string json, Type type)
		{
			return JsonConvert.DeserializeObject(json, type);
		}

		/// <summary>
		/// Deserialize given string json to an object.
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static object JsonParse(string json)
		{
			object result;
			try {
				result = JsonConvert.DeserializeObject(json);
			}
			catch (Exception) {
				result = null;
			}
			return result;
		}

		public static T JsonParse<T>(string json)
		{
			if (!string.IsNullOrEmpty(json))
				return JsonConvert.DeserializeObject<T>(json);
			else
				return Activator.CreateInstance<T>();
		}

		public static T JsonParse<T>(string json, bool ignoreRoot) where T : class
		{
			return ignoreRoot ? JObject.Parse(json)?.Properties()?.First()?.Value?.ToObject<T>() : JObject.Parse(json)?.ToObject<T>();
		}

		public static void JsonPopulate(string value, object target, bool setToDefault = false)
		{
			try {
				if (!string.IsNullOrEmpty(value)) {
					var jsonSerializerSettings = new JsonSerializerSettings {
						DefaultValueHandling = setToDefault ? DefaultValueHandling.Populate : DefaultValueHandling.Ignore,
						NullValueHandling = NullValueHandling.Include,
						ObjectCreationHandling = ObjectCreationHandling.Replace
					};

					JsonConvert.PopulateObject(value, target, jsonSerializerSettings);
				}
			}
			catch { }
		}

		public static string JsonStringify(object val, bool nullAsString = false, bool includeAllProperties = true)
		{
			if (string.IsNullOrEmpty(Convert.ToString(val)) && !nullAsString)
				return null;

			var jsonSerializerSettings = new JsonSerializerSettings {
				StringEscapeHandling = StringEscapeHandling.EscapeHtml,
				NullValueHandling = includeAllProperties ? NullValueHandling.Include : NullValueHandling.Ignore,
				DefaultValueHandling = includeAllProperties ? DefaultValueHandling.Include : DefaultValueHandling.Ignore,
				ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
				PreserveReferencesHandling = PreserveReferencesHandling.None,
				Formatting = Newtonsoft.Json.Formatting.None
			};

			return JsonStringify(val, jsonSerializerSettings);
		}

		public static string JsonStringify(object val, JsonSerializerSettings settings)
		{
			return JsonConvert.SerializeObject(val, settings);
		}

		public static string JsonStringifyPartially(object val, params string[] propertyNames)
		{
			if (val == null)
				return null;

			var jsonResolver = new PropertySerializableSerializerContractResolver();
			jsonResolver.SerializableProperty(val.GetType(), propertyNames);

			var jsonSerializerSettings = new JsonSerializerSettings {
				StringEscapeHandling = StringEscapeHandling.EscapeHtml,
				NullValueHandling = NullValueHandling.Ignore,
				DefaultValueHandling = DefaultValueHandling.Ignore,
				ContractResolver = jsonResolver,
				Formatting = Newtonsoft.Json.Formatting.None
			};

			var result = JsonStringify(val, jsonSerializerSettings);

			return result;
		}

		public static dynamic GetAsParameters(string source)
		{
			if (string.IsNullOrEmpty(source))
				return null;

			var result = Newtonsoft.Json.Linq.JObject.Parse(source);
			return result;
		}
	}

	public class PropertySerializableSerializerContractResolver : DefaultContractResolver
	{
		private readonly Dictionary<Type, HashSet<string>> _serializable;

		public PropertySerializableSerializerContractResolver()
		{
			_serializable = new Dictionary<Type, HashSet<string>>();
		}

		public void SerializableProperty(Type type, params string[] jsonPropertyNames)
		{
			if (!_serializable.ContainsKey(type))
				_serializable[type] = new HashSet<string>();

			foreach (var prop in jsonPropertyNames)
				_serializable[type].Add(prop);
		}

		protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
		{
			var properties = base.CreateProperties(type, memberSerialization);
			properties = properties.Where(p => IsSerializable(type, p.PropertyName)).ToList();

			return properties;
		}

		private bool IsSerializable(Type type, string jsonPropertyName)
		{
			if (!_serializable.ContainsKey(type))
				return false;

			return _serializable[type].Contains(jsonPropertyName);
		}
	}
}
