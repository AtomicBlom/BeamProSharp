using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace BinaryVibrance.Beam.API.Messages.User
{
	[PublicAPI]
	public class FindUserByIdRequest : GetMessageBase
	{
		[JsonIgnore]
		public int UserId { get; private set; }

		public FindUserByIdRequest(int userId)
		{
			UserId = userId;
		}

		protected override string Uri => $"users/{UserId}";
	}
}
