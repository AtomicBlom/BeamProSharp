using System;
using System.Diagnostics;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace BinaryVibrance.Beam.API.Model
{
	[PublicAPI]
	[DebuggerDisplay("ChannelType Id:{Id}, Url:{Url}")]
	public class Avatar
	{
		[JsonProperty("createdAt")]
		public DateTime Created { get; private set; }

		public int Id { get; private set; }

		[JsonProperty("meta")]
		public AvatarMetadata Metadata { get; private set; }

		[JsonProperty("relid")]
		public int RelativeId { get; private set; }

		public Uri RemotePath { get; private set; }
		public string Store { get; private set; }
		public string Type { get; private set; }

		[JsonProperty("updatedAt")]
		public DateTime Updated { get; private set; }

		public Uri Url { get; private set; }
	}
}