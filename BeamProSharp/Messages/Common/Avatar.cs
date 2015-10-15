using System;
using Newtonsoft.Json;

namespace BinaryVibrance.Beam.API.Messages.Common
{
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