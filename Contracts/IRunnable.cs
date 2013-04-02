using System;

namespace Contracts
{
    public interface IRunnable : IDisposable
    {
        void Run();
    }
    public abstract class Runnable : IRunnable
    {
        private readonly Scheduler scheduler;
        private bool disposed = false;
        public LoggerWriter Logger { get; set; }

        public Runnable(Scheduler scheduler)
        {
            this.scheduler = scheduler;
        }

        public void Run()
        {
            if(scheduler.CanBeExecuted())
                Body();
        }

        public abstract void Body();
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public abstract void OnDispose();
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    OnDispose();
                }
                disposed = true;

            }
        }
        ~Runnable()
        {
            Dispose(false);
        }
    }
}