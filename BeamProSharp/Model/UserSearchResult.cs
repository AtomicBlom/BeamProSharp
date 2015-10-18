using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BinaryVibrance.Beam.API.Model
{
	public class UserSearchResult
	{
		[JsonProperty("id")]
		public int UserId { get; private set; }
		
		public string Username { get; private set; }
	}
}
