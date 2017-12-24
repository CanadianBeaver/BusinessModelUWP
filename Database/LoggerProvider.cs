#if DEBUG

namespace Database
{
	using Microsoft.Extensions.Logging;
	using System;
	using System.Diagnostics;

	internal class LoggerProvider : ILoggerProvider
	{
		public ILogger CreateLogger(string categoryName)
		{
			return new ConsoleLogger();
		}

		public void Dispose() { }

		private class ConsoleLogger : ILogger
		{
			public bool IsEnabled(LogLevel logLevel)
			{
				return logLevel == LogLevel.Information;
			}

			public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
			{
				Debug.WriteLine($"EFCore ({logLevel.ToString()}): {formatter(state, exception)}");
			}

			public IDisposable BeginScope<TState>(TState state)
			{
				return null;
			}
		}
	}
}

#endif