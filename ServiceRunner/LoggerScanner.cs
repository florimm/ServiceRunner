using System;
using System.Linq;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace ServiceRunner
{
    public class LoggerScanner : IRegistrationConvention
    {
        private readonly string loggerSettings;
        public LoggerScanner()
        {
            var key =
                System.Configuration.ConfigurationManager.AppSettings.AllKeys.SingleOrDefault(t => t == "Logger");
            if(key != null)
            {
                loggerSettings = System.Configuration.ConfigurationManager.AppSettings[key];
            }
        }

        public void Process(Type type, Registry registry)
        {
            if(string.IsNullOrEmpty(loggerSettings) || loggerSettings == "ALL")
            {
                registry.AddType(type);
            }
            else
            {
                var loggers = loggerSettings.Split(',');
                if(loggers.Any(c=> c.ToLower() == type.Name.ToLower()))
                {
                    registry.AddType(type);
                }
            }
        }
    }
}