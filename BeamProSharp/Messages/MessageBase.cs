namespace BinaryVibrance.Beam.API.Messages
{
	public abstract class MessageBase
	{
		protected abstract string Uri { get; }

		internal string GetUri()
		{
			return Uri;
		}
	}
}