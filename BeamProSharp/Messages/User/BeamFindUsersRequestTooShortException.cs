using System;

namespace BinaryVibrance.Beam.API.Messages.User
{
	public class BeamFindUsersRequestTooShortException : Exception
	{
		public BeamFindUsersRequestTooShortException() : base("User searches should be more than 1 character.")
		{
			
		}
	}
}