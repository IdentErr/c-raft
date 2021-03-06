﻿#region C#raft License
// This file is part of C#raft. Copyright C#raft Team 
// 
// C#raft is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License as
// published by the Free Software Foundation, either version 3 of the
// License, or (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Affero General Public License for more details.
// 
// You should have received a copy of the GNU Affero General Public License
// along with this program. If not, see <http://www.gnu.org/licenses/>.
#endregion
using System;
using Chraft.Properties;
using System.IO;
using Chraft.Plugins.Events.Args;
using Chraft.Plugins.Events;

namespace Chraft
{
	public class Logger
	{
		private StreamWriter WriteLog;
        private Server Server;

		internal Logger(Server server, string file)
		{
            Server = server;
			try
			{
				WriteLog = new StreamWriter(file, true);
				WriteLog.AutoFlush = true;
			}
			catch
			{
				WriteLog = null;
			}
		}

		~Logger()
		{
			try
			{
				WriteLog.Close();
			}
			catch
			{
			}
		}

        public void Log(LogLevel level, string format, params object[] arguments)
        {
			Log(level, string.Format(format, arguments));
        }

        public void Log(LogLevel level, string message)
		{
            //Event
            LoggerEventArgs e = new LoggerEventArgs(this, level, message);
            Server.PluginManager.CallEvent(Event.LoggerLog, e);
            // do not allow cancellation or altering of log messages
            //End Event

            LogToConsole(level, message);
			LogToFile(level, message);
		}

		private void LogToConsole(LogLevel level, string message)
		{
			if ((int)level >= Settings.Default.LogConsoleLevel)
				Console.WriteLine(Settings.Default.LogConsoleFormat, DateTime.Now, level.ToString().ToUpper(), message);
		}

		private void LogToFile(LogLevel level, string message)
		{
			if ((int)level >= Settings.Default.LogFileLevel && WriteLog != null)
				WriteLog.WriteLine(Settings.Default.LogFileFormat, DateTime.Now, level.ToString().ToUpper(), message);
		}

		public void Log(Exception ex)
		{
            //Event
            LoggerEventArgs e = new LoggerEventArgs(this, LogLevel.Debug, ex.ToString(), ex);
            Server.PluginManager.CallEvent(Event.LoggerLog, e);
            // do not allow cancellation or altering of log messages
            //End Event

			Log(LogLevel.Debug, ex.ToString());
		}


		public enum LogLevel : int
		{
			Trivial = -1,
			Debug = 0,
			Info = 1,
			Warning = 2,
			Caution = 3,
			Notice = 4,
			Error = 5,
			Fatal = 6
		}
	}
}