using System;
using System.Collections.Generic;

namespace Contracts
{
    public class LoggerWriter
    {
        private readonly IEnumerable<ILogger> loggers;

        public LoggerWriter(IEnumerable<ILogger> loggers)
        {
            this.loggers = loggers;
        }


        public void Debug(string msg, Exception ex)
        {
            foreach (var logger in loggers)
            {
                logger.Debug(msg, ex);
            }
        }

        public void Info(string msg)
        {
            foreach (var logger in loggers)
            {
                logger.Info(msg);
            }
        }

        public void Error(string msg, Exception ex)
        {
            foreach (var logger in loggers)
            {
                logger.Error(msg, ex);
            }
        }

        public void Fatal(string msg, Exception ex)
        {
            foreach (var logger in loggers)
            {
                logger.Fatal(msg, ex);
            }
        }
    }
}