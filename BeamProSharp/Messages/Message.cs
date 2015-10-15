namespace BinaryVibrance.Beam.API.Messages
{
	public abstract class Message
	{
		protected abstract string Uri { get; }

		internal string GetUri()
		{
			return Uri;
		}
	}
}