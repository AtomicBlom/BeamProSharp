using System.Collections.Generic;
using BinaryVibrance.Beam.API.Model;
using JetBrains.Annotations;

namespace BinaryVibrance.Beam.API.Messages.User
{
	[PublicAPI]
	public class FindUsersByPartialNameResponse : List<UserSearchResult>, IMessageResponse<FindUsersByPartialNameRequest>
	{

	}
}