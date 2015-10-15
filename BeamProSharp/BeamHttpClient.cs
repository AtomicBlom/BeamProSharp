using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BinaryVibrance.Beam.API.Messages;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace BinaryVibrance.Beam.API
{
	public class BeamHttpClient
	{
		private readonly HttpClient _client;
		private readonly JsonSerializerSettings _settings;
		private readonly MemoryTraceWriter _traceWriter;

		public BeamHttpClient()
		{
			_client = new HttpClient
			{
				BaseAddress = BeamAPI.BasePath
			};

			_traceWriter = new MemoryTraceWriter();
			_settings = new JsonSerializerSettings
			{
				TraceWriter = _traceWriter,
				MissingMemberHandling = MissingMemberHandling.Ignore,
				ContractResolver = new JavaScriptContractResolver(),
				NullValueHandling = NullValueHandling.Ignore
			};
		}

		public async Task<TResponse> Get<TGetRequest, TResponse>(TGetRequest message)
			where TGetRequest : GetMessage
			where TResponse : IMessageResponse<TGetRequest>
		{
			try
			{
				var messageUri = GetMessageUri(message);
				var response = await _client.GetAsync(messageUri);
				return await DeserializeResponse<TResponse>(response);
			}
			finally
			{
				Console.WriteLine(_traceWriter);
			}
		}

		public async Task<TResponse> Post<TPostRequest, TResponse>(TPostRequest message)
			where TPostRequest : PostMessage
			where TResponse : IMessageResponse<TPostRequest>
		{
			ITraceWriter traceWriter = new MemoryTraceWriter();

			try
			{
				var messageUri = message.GetUri();
				var response = await _client.PostAsync(messageUri, PostMessageBody(message));

				return await DeserializeResponse<TResponse>(response);
			}
			finally
			{
				Console.WriteLine(traceWriter);
			}
		}

		private async Task<TResponse> DeserializeResponse<TResponse>(HttpResponseMessage response)
		{
			var responseContent = await response.Content.ReadAsStringAsync();
			Console.WriteLine(responseContent);

			switch (response.StatusCode)
			{
				case HttpStatusCode.OK:

					return JsonConvert.DeserializeObject<TResponse>(responseContent, _settings);
				default:
					throw new BeamException($"Unknown Status Code: {response.StatusCode}");
			}
		}

		private string GetMessageUri(Message message)
		{
			var uri = new StringBuilder();
			uri.Append(message.GetUri());

			var firstMember = true;
			foreach (var parameter in GetObjectValues(message))
			{
				uri.Append(firstMember ? '?' : '&');
				firstMember = false;

				uri.Append(parameter.Key);
				uri.Append('=');
				uri.Append(parameter.Value);
			}

			return uri.ToString();
		}

		private MultipartFormDataContent PostMessageBody(Message message)
		{
			var request = new MultipartFormDataContent();

			foreach (var parameter in GetObjectValues(message))
			{
				request.Add(new StringContent(parameter.Value), parameter.Key);
			}

			return request;
		}

		private static IEnumerable<KeyValuePair<string, string>> GetObjectValues(Message message)
		{
			foreach (
				var memberInfo in
					message.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty))
			{
				var memberValue = memberInfo?.GetMethod.Invoke(message, null)?.ToString();

				if (!string.IsNullOrEmpty(memberValue))
				{
					var name = memberInfo.Name;
					name = name[0].ToString().ToLowerInvariant() + name.Substring(1);
					yield return new KeyValuePair<string, string>(name, memberValue);
				}
			}
		}
	}
}