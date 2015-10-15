using System.Collections.Generic;
using BinaryVibrance.Beam.API.Model;
using JetBrains.Annotations;

namespace BinaryVibrance.Beam.API.Messages.Channel
{
	[PublicAPI]
	public class ChannelTypesResponse : List<ChannelType>, IMessageResponse<ChannelTypesRequest>
	{
	}
}