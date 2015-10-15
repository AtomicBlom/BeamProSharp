using JetBrains.Annotations;

namespace BinaryVibrance.Beam.API.Messages.User
{
	[PublicAPI]
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

		protected override string Uri => "users/login";
	}

	[PublicAPI]
	public class LoginResponse : Common.User, IMessageResponse<LoginRequest>
	{
		
	}
}