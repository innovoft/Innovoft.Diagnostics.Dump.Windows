using System;
using System.Collections.Generic;
using System.Text;

using Innovoft.Diagnostics;

namespace Innovoft.Diagnostics
{
	internal class Program
	{
		#region Main
		private static int Main(string[] args)
		{
			Dump.WriteThrow("dump.dmp", DumpType.WithFullMemory);
			return 0;
		}
		#endregion //Main
	}
}
