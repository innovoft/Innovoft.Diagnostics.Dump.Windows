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
			var wrote = Dump.Write("dump.dmp", DumpType.WithFullMemory, out var error);
			if (!wrote)
			{
				Console.WriteLine("Could not write dump.dmp Error: {0:X}");
			}
			return 0;
		}
		#endregion //Main
	}
}
