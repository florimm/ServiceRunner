using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using Contracts;
using StructureMap;

namespace ServiceRunner
{
    public partial class Executer : ServiceBase
    {
        public Executer()
        {
            InitializeComponent();
            ObjectFactory.Initialize(c=>
                {
                    c.Scan(sc=>
                        {
                            sc.AssemblyContainingType<IRunnable>();
                            sc.AddAllTypesOf<IRunnable>().NameBy(t=> t.Name);
                            sc.With(new RunnableScanner());
                        });
                    c.Scan(sc =>
                    {
                        sc.AssemblyContainingType<Scheduler>();
                        sc.AddAllTypesOf<Scheduler>().NameBy(t=> t.Name);
                    });
                   
                    c.Scan(sc=>
                        {
                            sc.AssemblyContainingType<ILogger>();
                            sc.AddAllTypesOf<ILogger>();
                            sc.With(new LoggerScanner());
                        });
                    c.For<LoggerWriter>().Use(t => new LoggerWriter(t.GetAllInstances<ILogger>()));
                });
        }

        protected override void OnStart(string[] args)
        {
            executeEvery.Interval = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["interval"]);
            executeEvery.Enabled = true;
        }

        protected override void OnStop()
        {
            ObjectFactory.ReleaseAndDisposeAllHttpScopedObjects();
        }

        private void executeEvery_Tick(object sender, EventArgs e)
        {
            var runnubles = ObjectFactory.GetAllInstances<IRunnable>();
            foreach (var runnuble in runnubles)
            {
                runnuble.Run();
            }
        }
    }
}
