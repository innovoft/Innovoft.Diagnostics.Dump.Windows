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
			var wrote = Dump.Write("dump.dmp", DumpType.WithFullMemory);
			if (!wrote)
			{
				Console.WriteLine("Could not write dump.dmp");
			}
			return 0;
		}
		#endregion //Main
	}
}
