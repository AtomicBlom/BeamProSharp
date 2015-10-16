using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace BinaryVibrance.Beam.API.Messages.User
{
	[PublicAPI]
	public class ChangeAvatarRequest : PostMessageBase
	{
		public ChangeAvatarRequest(int id, byte[] image)
		{
			Id = id;
			Image = image;
		}

		protected override string Uri => $"users/{Id}/changeAvatar";

		public int Id { get; private set; }
		public byte[] Image { get; private set; }
	}
}
