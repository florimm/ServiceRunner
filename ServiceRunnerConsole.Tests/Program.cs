using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using StructureMap;

namespace ServiceRunnerConsole.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            ObjectFactory.Initialize(c =>
            {
                c.Scan(sc =>
                {
                    sc.AssemblyContainingType<Scheduler>();
                    sc.AddAllTypesOf<Scheduler>().NameBy(t => t.Name);
                });
                c.Scan(sc =>
                {
                    sc.AssembliesFromApplicationBaseDirectory();
                    sc.AddAllTypesOf<IRunnable>();
                    sc.With(new RunnableScanner());

                });
                c.Scan(sc =>
                {
                    sc.AssemblyContainingType<ILogger>();
                    sc.AddAllTypesOf<ILogger>();
                    sc.With(new LoggerScanner());
                });
                c.For<LoggerWriter>().Use(t => new LoggerWriter(t.GetAllInstances<ILogger>()));
            });
            var r = ObjectFactory.GetNamedInstance<IRunnable>("GSM");
            using (r)
            {
                r.Run();
                r.Run();
                r.Run();
            }
            //System.Console.Write(ObjectFactory.WhatDoIHave());

            Console.ReadLine();
        }
    }
}
