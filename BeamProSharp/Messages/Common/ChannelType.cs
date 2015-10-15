using System.Diagnostics;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace BinaryVibrance.Beam.API.Messages.Channel
{
	[PublicAPI]
	[DebuggerDisplay("ChannelType Id:{Id}, Name:{Name}")]
	public class ChannelType
	{
		//[JsonProperty("id")]
		public int Id { get; private set; }

		//[JsonProperty("name")]
		public string Name { get; private set; }

		[JsonProperty("parent")]
		public string ParentCategory { get; private set; }

		//[JsonProperty("description")]
		public string Description { get; private set; }

		//[JsonProperty("source")]
		public string Source { get; private set; }

		[JsonProperty("viewersCurrent")]
		public int CurrentViewers { get; private set; }

		[JsonProperty("online")]
		public int OnlineStreamers { get; private set; }
	}
}