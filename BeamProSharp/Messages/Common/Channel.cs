using System;
using System.Diagnostics;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace BinaryVibrance.Beam.API.Messages.User
{
	[PublicAPI]
	[DebuggerDisplay("Channel Id:{Id}, Name:{Name}")]
	public class Channel
	{
		public Audience Audience { get; private set; }
		public int CoverId { get; private set; }
		[JsonProperty("createdAt")]
		public DateTime Created { get; private set; }
		public string Description { get; private set; }
		public bool Featured { get; private set; }
		public int Id { get; private set; }
		public string Name { get; private set; }
		[JsonProperty("numFollowers")]
		public int Followers { get; private set; }
		[JsonProperty("numSubscribers")]
		public int Subscribers { get; private set; }
		public bool Online { get; private set; }
		public bool Partnered { get; private set; }
		public bool Suspended { get; private set; }
		public int ThumbnailId { get; private set; }
		public string Token { get; private set; }
		public bool TranscodingEnabled { get; private set; }
		[JsonProperty("updatedAt")]
		public DateTime Updated { get; private set; }
		public int UserId { get; private set; }
		public int ViewersCurrent { get; private set; }
		public int ViewersTotal { get; private set; }
	}
}