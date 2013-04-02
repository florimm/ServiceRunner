using System;

namespace Contracts
{
    public class ConsoleLogger : ILogger
    {
        public void Debug(string msg, Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(msg);
        }

        public void Info(string msg)
        {
            System.Diagnostics.Debug.WriteLine(msg);
        }

        public void Error(string msg, Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(msg);
        }

        public void Fatal(string msg, Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(msg);
        }
    }
}