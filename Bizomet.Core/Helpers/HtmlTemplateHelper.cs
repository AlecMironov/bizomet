//namespace Bizomet.Core.Helpers
//{
//	public class HtmlTemplateHelper
//	{
//		public static string MergeHtmlWithJson(string json, string htmlTemplate)
//		{
//			var parsedJson = JObject.Parse(json);
//			var jsonKeys = ConvertJsonToDictionary(parsedJson, new Dictionary<string, string>());

//			return MergeHtmlWithJson(jsonKeys, htmlTemplate);
//		}

//		public static string MergeHtmlWithJson(Dictionary<string, string> jsonKeys, string htmlTemplate)
//		{
//			foreach (var key in jsonKeys) {
//				htmlTemplate = htmlTemplate.Replace("{{" + key.Key + "}}", key.Value);
//			}

//			return htmlTemplate;
//		}

//		public static Dictionary<string, string> ConvertJsonToDictionary(JObject parsedJson, Dictionary<string, string> jsonKeys, string path = "")
//		{
//			foreach (var rootItem in parsedJson) {
//				if (rootItem.Value.HasValues) {
//					foreach (var value in rootItem.Value) {
//						string keyValue = string.Empty;
//						if (value.Count() == 1) {
//							keyValue = value.First.ToString();

//							if (keyValue.Contains("{") || keyValue.Contains("}")) {
//								string[] arrayPath = value.Path.Split('.');

//								if (arrayPath.Length > 1) {
//									arrayPath = arrayPath.Take(arrayPath.Count() - 1).ToArray();
//								}

//								string innerPropPath = string.Join(".", arrayPath);
//								innerPropPath = !string.IsNullOrWhiteSpace(path) ? $"{path}.{innerPropPath}" : innerPropPath;
//								ConvertJsonToDictionary(JObject.Parse("{" + value.ToString() + "}"), jsonKeys, innerPropPath);
//							}
//							else {
//								if (!string.IsNullOrWhiteSpace(path)) {
//									jsonKeys[$"{path}.{value.Path}"] = keyValue;
//								}
//								else {
//									jsonKeys[value.Path] = keyValue;
//								}
//							}
//						}
//						else {
//							foreach (var item in value) {
//								keyValue = item.First.ToString();

//								if (!string.IsNullOrWhiteSpace(path)) {
//									jsonKeys[$"{path}.{item.Path}"] = keyValue;
//								}
//								else {
//									jsonKeys[item.Path] = keyValue;
//								}
//							}
//						}
//					}
//				}
//				else {
//					jsonKeys[rootItem.Value.Path] = rootItem.Value.ToString();
//				}
//			}

//			return jsonKeys;
//		}
//	}
//}
