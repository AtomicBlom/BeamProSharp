using System;
using BinaryVibrance.Beam.API.Messages.User;
using Newtonsoft.Json;

namespace BinaryVibrance.Beam.API.Converter
{
	public class AudienceConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			switch ((Audience) value)
			{
				case Audience.EighteenPlus:
					writer.WriteValue("18+");
					break;
				case Audience.G:
					writer.WriteValue("G");
					break;
				default:
					throw new Exception($"Unknown Audience: {value}");
			}
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var value = reader.Value as string;
			switch (value)
			{
				case "G":
					return Audience.G;
				case "18+":
					return Audience.EighteenPlus;
				default:
					throw new Exception($"Unknown Audience: {value}");
			}
		}

		public override bool CanConvert(Type objectType)
		{
			throw new NotImplementedException();
		}
	}
}