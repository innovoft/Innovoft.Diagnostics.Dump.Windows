using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Innovoft.Diagnostics
{
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	internal struct ExceptionInfo
	{
		public uint ThreadId;
		public IntPtr ExceptionPointers;
		[MarshalAs(UnmanagedType.Bool)]
		public bool ClientPointers;
	}
}
