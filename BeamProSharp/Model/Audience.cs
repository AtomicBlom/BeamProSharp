using BinaryVibrance.Beam.API.Converter;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace BinaryVibrance.Beam.API.Model
{
	[PublicAPI]
	[JsonConverter(typeof (AudienceConverter))]
	public enum Audience
	{
		//FIXME: Need to find the other values for this.
		G,

		EighteenPlus
	}
}