using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryVibrance.Beam.API.Services
{
	public class ConsoleLogger : ILogFacade
	{
		public void Trace(string message)
		{
			Console.WriteLine($"[TRACE] - {message}");
		}

		public void Info(string message)
		{
			Console.WriteLine($"[INFO ] - {message}");
		}
	}
}
