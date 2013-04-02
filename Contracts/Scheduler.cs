using System;

namespace Contracts
{
    public abstract class Scheduler
    {
        public Scheduler()
        {
            this.Interval = 1;
        }
        public DateTime? LastExecuted { get; set; }
        public int Interval { get; set; }
        public abstract ExecuteType ExecuteType { get;}
        public abstract bool CanBeExecuted();
    }

    public class ModuleException : Exception
    {
        public ModuleException(string message) : base(message)
        {
            
        }
    }
}