using System.Collections.Generic;
using JetBrains.Annotations;

namespace BinaryVibrance.Beam.API.Messages.Channel
{
	[PublicAPI]
	public class ChannelTypesRequest : GetMessage
	{
		protected override string Uri => "types";

		public string Query { get; set; }
	}

	[PublicAPI]
	public class ChannelTypesResponse : List<ChannelType>, IMessageResponse<ChannelTypesRequest>
	{
	}
}