using System;
using System.Collections.Generic;
using System.Linq;
using Contracts;
using StructureMap;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;
using StructureMap.Pipeline;

namespace ServiceRunnerConsole.Tests
{
    public class RunnableScanner : IRegistrationConvention
    {
        private readonly List<Module> modules; 
        public RunnableScanner()
        {
            var config = RegisterModulesConfig.GetConfig();
            modules = config.Modules.ToList();
        }

        public void Process(Type type, Registry registry)
        {
            if (!type.IsAbstract && typeof(IRunnable).IsAssignableFrom(type))
            {
                var module = modules.SingleOrDefault(c => c.Name == type.Name);
                if (module != null)
                {
                    registry.For(typeof (IRunnable)).Use(type).Named(type.Name).CtorDependency<Scheduler>("scheduler")
                        .Is(new LambdaInstance<Scheduler>(c =>
                          {
                              var sch = c.GetInstance<Scheduler>(module.Scheduler + "Scheduler");
                              sch.Interval = module.Interval;
                              return sch;
                          }));
                }
            }
        }
    }
}