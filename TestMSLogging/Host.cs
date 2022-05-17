using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace TestMSLogging
{
    internal class Host : IDisposable
    {
        private readonly ILoggerFactory logFactoryWithCustomConsole;
        private readonly ILoggerFactory logFactory;

        internal Host()
        {
            logFactoryWithCustomConsole =
                LoggerFactory.Create(builder =>
                    builder
                        .SetMinimumLevel(LogLevel.Trace)
                        .AddConsole(config => config.FormatterName = nameof(CliConsoleFormatter))
                        .AddConsoleFormatter<CliConsoleFormatter, ConsoleFormatterOptions>(config =>
                        {
                            config.IncludeScopes = true;
                            config.TimestampFormat = "yyyy-MM-dd HH:mm:ss.fff";
                        }));

            logFactory =
                LoggerFactory.Create(builder =>
                    builder
                        .SetMinimumLevel(LogLevel.Trace)
                        .AddSimpleConsole(options =>
                        {
                            options.IncludeScopes = true;
                            options.SingleLine = true;
                            options.TimestampFormat = "hh:mm:ss ";
                        }));
        }

        public ILoggerFactory LogFactoryWithCustomConsole => logFactoryWithCustomConsole;
        public ILoggerFactory LogFactory => logFactory;

        public void Dispose()
        {
            logFactoryWithCustomConsole.Dispose();
            logFactory.Dispose();
        }
    }
}
