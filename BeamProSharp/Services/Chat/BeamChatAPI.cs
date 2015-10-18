using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BinaryVibrance.Beam.API.Messages.Chat;

namespace BinaryVibrance.Beam.API.Services.Chat
{
	internal class BeamChatAPI : IUnauthenticatedChatTasks
	{
		private readonly BeamHttpClient _client;
		private int _channelId;
		private string _authorizationKey;
		private IEnumerable<string> _permissions;
		private Queue<Uri> _endpoints;
		private ClientWebSocket _webSocket;
		private Thread _receiveThread;
		private bool _channelJoined;
		private CancellationToken _cancellationToken;

		public BeamChatAPI(BeamHttpClient client)
		{
			_client = client;
		}

		public async Task Join(int channelId)
		{
			_channelId = channelId;
			JoinRequest request = new JoinRequest(channelId);
			var response = await _client.Get<JoinRequest, JoinResponse>(request);

			_authorizationKey = response.AuthorizationKey;
			_permissions = response.Permissions;
			_endpoints = new Queue<Uri>(response.Endpoints.OrderBy(_ => Guid.NewGuid()));

			await Connect();
		}

		private async Task Connect()
		{
			var nextEndPoint = _endpoints.Dequeue();
			_endpoints.Enqueue(nextEndPoint);

			_webSocket = new ClientWebSocket();
			await _webSocket.ConnectAsync(nextEndPoint, CancellationToken.None);
			Beam.Logger.Info($"Connected to endpoint {nextEndPoint}");
			_channelJoined = true;
			_cancellationToken = new CancellationToken();
			_receiveThread = new Thread(StartRecieving)
			{
				Name = $"Chat Receive Thread for {_channelId}"
			};

			_receiveThread.Start();
		}

		private async void StartRecieving()
		{
			try
			{
				while (!_cancellationToken.IsCancellationRequested)
				{
					ArraySegment<byte> buffer = new ArraySegment<byte>(new byte[1024]);

					WebSocketReceiveResult result;
					StringBuilder stringBuilder = new StringBuilder();
					do
					{
						result = await _webSocket.ReceiveAsync(buffer, _cancellationToken);
						if (result.MessageType == WebSocketMessageType.Close)
						{
							throw new BeamChatDisconnectedException();
						}

						stringBuilder.Append(Encoding.UTF8.GetString(buffer.Array, buffer.Offset, buffer.Count));
					} while (!result.EndOfMessage);

					string message = stringBuilder.ToString();
					Beam.Logger.Trace(message);
				}
				Console.WriteLine("bah");
			}
			catch (Exception e)
			{
				Console.WriteLine("bah");
				//FIXME: reconnect;
			}
			Console.WriteLine("bah");
		}
	}

	internal class BeamChatDisconnectedException : Exception
	{
	}
}