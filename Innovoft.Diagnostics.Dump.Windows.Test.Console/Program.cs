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
			Dump.Write("dump.dmp", DumpType.Normal);
			return 0;
		}
		#endregion //Main
	}
}
