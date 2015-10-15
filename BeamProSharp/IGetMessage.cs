using System;

namespace BinaryVibrance.Beam.API
{
	public abstract class Message
	{
		protected abstract string Uri { get; }

		internal string GetUri()
		{
			return Uri;
		}
	}

	public abstract class GetMessage : Message { }
	public abstract class PostMessage : Message { }
}