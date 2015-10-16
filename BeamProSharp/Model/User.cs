using System;
using System.Collections.Generic;
using System.Diagnostics;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace BinaryVibrance.Beam.API.Model
{
	[PublicAPI]
	[DebuggerDisplay("User Id:{Id}, Name:{Username}")]
	public class User
	{
		[JsonProperty("avatars")]
		public List<Avatar> Avatars { get; private set; }

		public string Bio { get; private set; }

		public Channel Channel { get; private set; }

		[JsonProperty("createdAt")]
		public DateTime Created { get; private set; }

		[JsonProperty("deletedAt")]
		public DateTime Deleted { get; private set; }

		public string Email { get; private set; }

		public IEnumerable<Group> Groups { get; private set; }

		public bool HasTwoFactor { get; private set; }

		public int Id { get; private set; }

		public int Points { get; private set; }

		public UserPreferences Preferences { get; private set; }

		public Dictionary<string, string> Social { get; private set; }

		[JsonProperty("updatedAt")]
		public DateTime Updated { get; private set; }

		public string Username { get; private set; }
		public bool Verified { get; private set; }
	}
}