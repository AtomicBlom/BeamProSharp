using JetBrains.Annotations;
using Newtonsoft.Json;

namespace BinaryVibrance.Beam.API.Messages.User
{
	[PublicAPI]
	public class FindUsersByPartialNameRequest : GetMessageBase
	{
		public FindUsersByPartialNameRequest(string partialName)
		{
			PartialName = partialName;
		}

		private string _partialName;
		private int _page;
		private int _limit;
		protected override string Uri => $"users/search";

		public int Page
		{
			get { return _page; }
			set
			{
				if (value < 0)
				{
					throw new BeamInvalidPageNumberException();
				}
				_page = value;
			}
		}

		public int Limit
		{
			get { return _limit; }
			private set
			{
				if (value < 0)
				{
					throw new BeamInvalidLimitException();
				}
				_limit = value;
			}
		}

		[JsonProperty("query")]
		public string PartialName
		{
			get { return _partialName; }
			private set
			{
				if (value.Length < 2)
				{
					throw new BeamFindUsersRequestTooShortException();
				}
				_partialName = value;
			}
		}
	}
}