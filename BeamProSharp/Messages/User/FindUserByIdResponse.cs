using JetBrains.Annotations;

namespace BinaryVibrance.Beam.API.Messages.User
{
	[PublicAPI]
	public class FindUserByIdResponse : Model.User, IMessageResponse<FindUserByIdRequest>
	{
		
	}
}