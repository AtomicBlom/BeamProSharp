using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Threading.Tasks;
using BinaryVibrance.Beam.API.Messages.Channel;
using BinaryVibrance.Beam.API.Messages.Chat;
using BinaryVibrance.Beam.API.Messages.User;
using BinaryVibrance.Beam.API.Model;
using BinaryVibrance.Beam.API.Services;

namespace BinaryVibrance.Beam.API
{
	public class Beam : IUnauthenticatedBeamTasks, IFindBeamTasks, IAuthenticatedBeamTasks, IFindBeamChannelTasks, IFindBeamUserTasks, IFindMultipleBeamUsersTasks
	{

		public static ILogFacade Logger = new NullLogger();

		private Beam()
		{
			
		}

		private BeamHttpClient _client = new BeamHttpClient();

		public static readonly Uri BasePath = new Uri("https://beam.pro/api/v1/");

		public static IUnauthenticatedBeamTasks Pro()
		{
			return new Beam();
		}

		IFindBeamTasks IUnauthenticatedBeamTasks.Find => this;

		async Task<IAuthenticatedBeamTasks> IUnauthenticatedBeamTasks.LoginAsync(string username, string password)
		{
			LoginRequest request = new LoginRequest(username, password);
			var response = await _client.Post<LoginRequest, LoginResponse>(request);
			SetUser(response);
			return this;
		}

		async Task<IAuthenticatedBeamTasks> IUnauthenticatedBeamTasks.LoginAsync(string username, string password, string twoFactorAuthenticationCode)
		{
			LoginRequest request = new LoginRequest(username, password, twoFactorAuthenticationCode);
			var response = await _client.Post<LoginRequest, LoginResponse>(request);
			SetUser(response);
			return this;
		}

		private User _user;
		

		#region IFindBeamTasks
		public IFindBeamChannelTasks Channel => this;
		public IFindBeamUserTasks User => this;
		public IFindMultipleBeamUsersTasks Users => this;
		#endregion

		User IAuthenticatedBeamTasks.User => _user;

		private void SetUser(User user)
		{
			_user = user;
		}

		private async Task<User> RefreshCurrentUser()
		{
			var response = await _client.Get<CurrentUserRequest, CurrentUserResponse>(new CurrentUserRequest());
			SetUser(response);
			return response;
		}

		BeamHttpClient IExposeBeamAPI.BeamClient => _client;

		#region Implementation of IFindBeamChannelTasks

		async Task<Channel> IFindBeamChannelTasks.ById(int channelId)
		{
			var request = new FindChannelByIdRequest(channelId);
			var response = await _client.Get<FindChannelByIdRequest, FindChannelByIdResponse>(request);
			return response;
		}

		async Task<IEnumerable<ChannelType>> IFindBeamChannelTasks.Types()
		{
			var response = await _client.Get<ChannelTypesRequest, ChannelTypesResponse>(new ChannelTypesRequest());
			return response;
		}

		async Task<IEnumerable<ChannelType>> IFindBeamChannelTasks.TypesByPartialName(string partialName)
		{
			var channelTypesRequest = new ChannelTypesRequest() { Query = partialName };
			var response = await _client.Get<ChannelTypesRequest, ChannelTypesResponse>(channelTypesRequest);
			return response;
		}

		#endregion

		#region Implementation of IFindBeamUserTasks

		async Task<User> IFindBeamUserTasks.ById(int userId)
		{
			var request = new FindUserByIdRequest(userId);
			var response = await _client.Get<FindUserByIdRequest, FindUserByIdResponse>(request);
			return response;
		}

		async Task<User> IFindBeamUserTasks.ByName(string name)
		{
			var request = new FindUsersByPartialNameRequest(name);
			var response = await _client.Get<FindUsersByPartialNameRequest, FindUsersByPartialNameResponse>(request);

			var user = response.SingleOrDefault(_ => _.Username.Equals(name));
			if (user == null)
			{
				throw new BeamUserNotFoundException();
			}

			return await ((IFindBeamUserTasks)this).ById(user.UserId);
		}

		#endregion

		#region Implementation of IFindMultipleBeamUsersTasks

		async Task<IEnumerable<UserSearchResult>> IFindMultipleBeamUsersTasks.ByPartialName(string partialName)
		{
			var request = new FindUsersByPartialNameRequest(partialName);
			var response = await _client.Get<FindUsersByPartialNameRequest, FindUsersByPartialNameResponse>(request);
			return response;
		}

		#endregion
	}

	public interface IUnauthenticatedBeamTasks : IExposeBeamAPI
	{
		IFindBeamTasks Find { get; }

		Task<IAuthenticatedBeamTasks> LoginAsync(string username, string password);
		Task<IAuthenticatedBeamTasks> LoginAsync(string username, string password, string twoFactorAuthenticationCode);
	}

	public interface IExposeBeamAPI
	{
		BeamHttpClient BeamClient { get; }
	}

	public interface IUnauthenticatedChatTasks
	{
	}

	public interface IAuthenticatedBeamTasks
	{
		User User { get; }
	}

	public interface IFindBeamTasks
	{
		IFindBeamChannelTasks Channel { get; }
		IFindBeamUserTasks User { get; }
		IFindMultipleBeamUsersTasks Users { get; }
	}


	public interface IFindBeamChannelTasks
	{
		Task<Channel> ById(int channelId);

		Task<IEnumerable<ChannelType>> Types();

		Task<IEnumerable<ChannelType>> TypesByPartialName(string partialName);
	}

	public interface IFindBeamUserTasks
	{
		Task<User> ById(int userId);
		Task<User> ByName(string name);
	}

	public interface IFindMultipleBeamUsersTasks
	{
		Task<IEnumerable<UserSearchResult>> ByPartialName(string partialName);
	}
}