using JetBrains.Annotations;

namespace BinaryVibrance.Beam.API.Messages.User
{
	[PublicAPI]
	//FIXME: Custom Deserializer
	public class LogoutReponse : IMessageResponse<LogoutRequest>
	{
		string Message { get; set; }
	}
}