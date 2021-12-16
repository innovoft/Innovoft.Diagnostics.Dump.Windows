using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Innovoft.Diagnostics
{
	public sealed class Dump
	{
		#region Methods
		public static void Write(string path, DumpType type)
		{
			using (var writer = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Read))
			using (var process = Process.GetCurrentProcess())
			{
				var exceptionInfo = new ExceptionInfo();
				exceptionInfo.ThreadId = GetCurrentThreadId();
				exceptionInfo.ExceptionPointers = Marshal.GetExceptionPointers();
				exceptionInfo.ClientPointers = false;
				var wrote = MiniDumpWriteDump(process.Handle, process.Id, writer.SafeFileHandle, type, ref exceptionInfo, IntPtr.Zero, IntPtr.Zero);
				if (!wrote)
				{
					throw Marshal.GetExceptionForHR(Marshal.GetHRForLastWin32Error());
				}
			}
		}

		[DllImport("kernel32.dll")]
		private static extern uint GetCurrentThreadId();

		[DllImport("dbghelp.dll", EntryPoint = "MiniDumpWriteDump", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
		private static extern bool MiniDumpWriteDump(IntPtr processHandle, int processId, SafeHandle file, DumpType type, ref ExceptionInfo exceptionInfo, IntPtr stream, IntPtr callback);
		#endregion //Methods
	}
}
