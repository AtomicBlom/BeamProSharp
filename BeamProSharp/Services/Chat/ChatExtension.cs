using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BinaryVibrance.Beam.API.Messages.Chat;

namespace BinaryVibrance.Beam.API.Services.Chat
{
	public static class ChatExtension
	{
		public static async Task<IUnauthenticatedChatTasks> JoinChatAsync(this IUnauthenticatedBeamTasks baseAPI, int channelId)
		{
			var client = baseAPI.BeamClient;
			var chatAPI = new BeamChatAPI(client);
			await chatAPI.Join(channelId);
			return chatAPI;
		}
	}
}
