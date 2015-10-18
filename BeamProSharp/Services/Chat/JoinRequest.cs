using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BinaryVibrance.Beam.API.Messages.Chat
{
	public class JoinRequest : GetMessageBase
	{
		private readonly int _channelId;

		public JoinRequest(int channelId)
		{
			_channelId = channelId;
		}

		protected override string Uri => $"chats/{_channelId}";
	}

	public class JoinResponse : IMessageResponse<JoinRequest>
	{
		public IEnumerable<Uri> Endpoints { get; private set; }

		[JsonProperty("authkey")]
		public string AuthorizationKey { get; private set; }

		public IEnumerable<string> Permissions { get; private set; } 

	}
}
