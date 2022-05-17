using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace TestMSLogging
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var messages = GenerateMessages();
            Host host = new Host();
            Console.WriteLine("Test logging with custom console formatter.");
            var loggerWithCustomCF = host.LogFactoryWithCustomConsole.CreateLogger<Program>();
            Log(loggerWithCustomCF, "custom console formatter", messages);

            //Console.WriteLine("Test logging with predefined SimpleConsole formatter.");
            //var logger = host.LogFactory.CreateLogger("SimpleConsole");
            //Log(logger, "SimpleConsole formatter", messages);
        }

        private static void Log(ILogger logger, string formatterName, string[] messages)
        {
            using (logger.BeginScope($"[Log with {formatterName}]"))
            {
                for (int i = 0; i < messages.Length; i++)
                {
                    logger.LogDebug(messages[i]);
                }
            }
        }

        private static string[] GenerateMessages()
        {
            int length = 50;
            var messages = new string[length];
            for(int i = 0; i < length; i++)
            {
                messages[i] = $"This is the message at index: {i} of the list";
            }
            return messages;
        }
    }
}
