using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BinaryVibrance.Beam.API;
using BinaryVibrance.Beam.API.Messages.Channel;
using BinaryVibrance.Beam.API.Messages.User;

namespace TestClient
{
	class Program
	{
		static void Main(string[] args)
		{
			/*BeamAPI api = new BeamAPI();
			Console.WriteLine("API Starting");
			var apiTask = api.Start();
			
			Console.WriteLine("Sending /types");
			api.SendMessage("/types");*/

			DoStuff().Wait();
		}

		static async Task DoStuff()
		{
			BeamHttpClient client = new BeamHttpClient();
			var channelTypesRequest = new ChannelTypesRequest()
			{
				Query = "Minecraft"
			};
			var channelTypesResponses = await client.Get<ChannelTypesRequest, ChannelTypesResponse>(channelTypesRequest);

			var loginRequest = new LoginRequest("AtomicBlom", "");
			var loginResponse = await client.Post<LoginRequest, LoginResponse>(loginRequest);

			Console.ReadKey();
		}
	}


	
}
