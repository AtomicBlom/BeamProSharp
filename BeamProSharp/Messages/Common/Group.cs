using System.Diagnostics;
using JetBrains.Annotations;

namespace BinaryVibrance.Beam.API.Messages.User
{
	[PublicAPI]
	[DebuggerDisplay("ChannelType Id:{Id}, Name:{Name}")]
	public class Group
	{
		public int Id { get; private set; }
		public string Name { get; private set; }
	}
}