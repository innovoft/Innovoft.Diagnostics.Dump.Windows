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
		#region Constants
		public const string Ext = "dmp";
		#endregion //Constants

		#region Methods
		public static void WriteException(string path, DumpType type)
		{
			using (var writer = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Read))
			using (var process = Process.GetCurrentProcess())
			{
				WriteException(writer, process, type);
			}
		}

		public static void WriteException(string path, Process process, DumpType type)
		{
			using (var writer = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Read))
			{
				WriteException(writer, process, type);
			}
		}

		public static void WriteException(FileStream writer, DumpType type)
		{
			using (var process = Process.GetCurrentProcess())
			{
				WriteException(writer, process, type);
			}
		}

		public static bool Write(string path, DumpType type)
		{
			using (var writer = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Read))
			using (var process = Process.GetCurrentProcess())
			{
				return Write(writer, process, type);
			}
		}

		public static bool Write(string path, Process process, DumpType type)
		{
			using (var writer = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Read))
			{
				return Write(writer, process, type);
			}
		}

		public static bool Write(FileStream writer, DumpType type)
		{
			using (var process = Process.GetCurrentProcess())
			{
				return Write(writer, process, type);
			}
		}

		public static bool Write(string path, DumpType type, out int error)
		{
			using (var writer = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Read))
			using (var process = Process.GetCurrentProcess())
			{
				return Write(writer, process, type, out error);
			}
		}

		public static bool Write(string path, Process process, DumpType type, out int error)
		{
			using (var writer = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Read))
			{
				return Write(writer, process, type, out error);
			}
		}

		public static bool Write(FileStream writer, DumpType type, out int error)
		{
			using (var process = Process.GetCurrentProcess())
			{
				return Write(writer, process, type, out error);
			}
		}

		public static void WriteException(FileStream writer, Process process, DumpType type)
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

		public static bool Write(FileStream writer, Process process, DumpType type)
		{
			var exceptionInfo = new ExceptionInfo();
			exceptionInfo.ThreadId = GetCurrentThreadId();
			exceptionInfo.ExceptionPointers = Marshal.GetExceptionPointers();
			exceptionInfo.ClientPointers = false;
			return MiniDumpWriteDump(process.Handle, process.Id, writer.SafeFileHandle, type, ref exceptionInfo, IntPtr.Zero, IntPtr.Zero);
		}

		public static bool Write(FileStream writer, Process process, DumpType type, out int error)
		{
			var exceptionInfo = new ExceptionInfo();
			exceptionInfo.ThreadId = GetCurrentThreadId();
			exceptionInfo.ExceptionPointers = Marshal.GetExceptionPointers();
			exceptionInfo.ClientPointers = false;
			var wrote = MiniDumpWriteDump(process.Handle, process.Id, writer.SafeFileHandle, type, ref exceptionInfo, IntPtr.Zero, IntPtr.Zero);
			if (wrote)
			{
				error = default;
				return true;
			}
			else
			{
				error = Marshal.GetHRForLastWin32Error();
				return false;
			}
		}

		[DllImport("kernel32.dll")]
		private static extern uint GetCurrentThreadId();

		[DllImport("dbghelp.dll", EntryPoint = "MiniDumpWriteDump", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
		private static extern bool MiniDumpWriteDump(IntPtr processHandle, int processId, SafeHandle file, DumpType type, ref ExceptionInfo exceptionInfo, IntPtr stream, IntPtr callback);
		#endregion //Methods
	}
}
