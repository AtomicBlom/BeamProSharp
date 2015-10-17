using System;
using System.Collections.Generic;
using System.Security;
using System.Threading.Tasks;
using BinaryVibrance.Beam.API.Messages.Channel;
using BinaryVibrance.Beam.API.Messages.User;
using BinaryVibrance.Beam.API.Model;

namespace BinaryVibrance.Beam.API
{
	public class Beam : IUnauthorizedBeamTasks, IFindBeamTasks, IAuthorizedBeamTasks
	{
		private Beam()
		{
			
		}

		private BeamHttpClient _client = new BeamHttpClient();

		public static readonly Uri BasePath = new Uri("https://beam.pro/api/v1/");

		public static IUnauthorizedBeamTasks Pro()
		{
			return new Beam();
		}

		IFindBeamTasks IUnauthorizedBeamTasks.Find => this;

		async Task<IAuthorizedBeamTasks> IUnauthorizedBeamTasks.LoginAsync(string username, string password)
		{
			LoginRequest request = new LoginRequest(username, password);
			var response = await _client.Post<LoginRequest, LoginResponse>(request);
			SetUser(response);
			return this;
		}

		async Task<IAuthorizedBeamTasks> IUnauthorizedBeamTasks.LoginAsync(string username, string password, string twoFactorAuthenticationCode)
		{
			LoginRequest request = new LoginRequest(username, password, twoFactorAuthenticationCode);
			var response = await _client.Post<LoginRequest, LoginResponse>(request);
			SetUser(response);
			return this;
		}


		public async Task<IEnumerable<ChannelType>> ChannelTypesAsync()
		{
			var response = await _client.Get<ChannelTypesRequest, ChannelTypesResponse>(new ChannelTypesRequest());
			return response;
		}

		private User _user;
		private DateTime _timeSinceLastUserRefresh = DateTime.MinValue;
		Task<User> IAuthorizedBeamTasks.User
		{
			get {
				if (DateTime.Now > _timeSinceLastUserRefresh.Add(TimeSpan.FromSeconds(30)))
				{
					return RefreshCurrentUser();
				}
				return Task.FromResult(_user);
			}
		}

		private void SetUser(User user)
		{
			_timeSinceLastUserRefresh = DateTime.Now;
			_user = user;
		}

		private async Task<User> RefreshCurrentUser()
		{
			var response = await _client.Get<CurrentUserRequest, CurrentUserResponse>(new CurrentUserRequest());
			SetUser(response);
			return response;
		}
	}

	public interface IUnauthorizedBeamTasks
	{
		IFindBeamTasks Find { get; }

		Task<IAuthorizedBeamTasks> LoginAsync(string username, string password);
		Task<IAuthorizedBeamTasks> LoginAsync(string username, string password, string twoFactorAuthenticationCode);
	}

	public interface IAuthorizedBeamTasks
	{
		Task<User> User { get; }
	}

	public interface IFindBeamTasks
	{
		Task<IEnumerable<ChannelType>> ChannelTypesAsync();
	}
}