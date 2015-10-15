using JetBrains.Annotations;

namespace BinaryVibrance.Beam.API.Messages.Channel
{
	[PublicAPI]
	public class ChannelTypesRequest : GetMessage
	{
		protected override string Uri => "types";

		public string Query { get; set; }
	}
}