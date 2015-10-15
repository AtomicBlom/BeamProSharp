using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace BinaryVibrance.Beam.API
{
	public class JavaScriptContractResolver : DefaultContractResolver
	{
		protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
		{
			//TODO: Maybe cache
			var prop = base.CreateProperty(member, memberSerialization);

			if (!prop.Writable)
			{
				var property = member as PropertyInfo;
				if (property != null)
				{
					var hasPrivateSetter = property.GetSetMethod(true) != null;
					prop.Writable = hasPrivateSetter;
				}
			}

			prop.PropertyName = prop.PropertyName[0].ToString().ToLowerInvariant() + prop.PropertyName.Substring(1);

			return prop;
		}
	}
}