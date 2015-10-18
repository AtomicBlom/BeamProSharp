using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace BinaryVibrance.Beam.API.Messages.Channel
{
	[PublicAPI]
	public class FindChannelByIdRequest : GetMessageBase
	{
		public int ChannelId { get; private set; }

		public FindChannelByIdRequest(int channelId)
		{
			ChannelId = channelId;
		}

		protected override string Uri => $"channels/{ChannelId}";
	}
}
