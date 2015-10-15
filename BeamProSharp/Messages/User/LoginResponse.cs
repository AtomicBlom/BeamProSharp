using JetBrains.Annotations;

namespace BinaryVibrance.Beam.API.Messages.User
{
	[PublicAPI]
	public class LoginResponse : Model.User, IMessageResponse<LoginRequest>
	{
	}
}