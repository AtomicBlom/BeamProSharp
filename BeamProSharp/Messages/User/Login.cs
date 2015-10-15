using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace BinaryVibrance.Beam.API.Messages.User
{
	public class LoginRequest : PostMessage
	{
		public LoginRequest(string username, string password)
		{
			Username = username;
			Password = password;
		}

		public LoginRequest(string username, string password, string twoFactorAuthenticationCode) : this(username, password)
		{
			TwoFactorAuthenticationCode = twoFactorAuthenticationCode;
		}

		public string TwoFactorAuthenticationCode { get; set; }
		public string Username { get; }
		public string Password { get; }

		protected override string Uri
		{
			get { return "users/login"; }
		}
	}

	public class LoginResponse : IMessageResponse<LoginRequest>
	{
		[JsonProperty("avatars")]
		public List<Avatar> Avatars { get; private set; }
	}

	public class Avatar
	{
		[JsonProperty("createdAt")]
		public DateTime Created { get; private set; }

		public int Id { get; private set; }

		[JsonProperty("meta")]
		public AvatarMetadata Metadata { get; private set; }

		[JsonProperty("relid")]
		public int RelativeId { get; private set; }

		public Uri RemotePath { get; private set; }

		public string Store { get; private set; }

		public string Type { get; private set; }

		[JsonProperty("updatedAt")]
		public DateTime Updated { get; private set; }

		public Uri Url { get; private set; }
	}

	public class AvatarMetadata
	{
		public string Size { get; private set; }
	}
}