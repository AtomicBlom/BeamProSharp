using JetBrains.Annotations;

namespace BinaryVibrance.Beam.API.Messages.Channel
{
	[PublicAPI]
	public class ChannelTypesRequest : GetMessageBase
	{
		protected override string Uri => "types";

		public string Query { get; set; }
	}
}