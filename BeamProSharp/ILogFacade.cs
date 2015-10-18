using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryVibrance.Beam.API
{
	public interface ILogFacade
	{
		void Trace(string message);
		void Info(string message);
	}
}
