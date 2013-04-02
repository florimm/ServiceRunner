using System;

namespace Contracts
{
    public class MinScheduler : Scheduler
    {
        public override ExecuteType ExecuteType
        {
            get
            {
                return ExecuteType.Min;
            }
        }
        public override bool CanBeExecuted()
        {
            if (!LastExecuted.HasValue)
            {
                LastExecuted = DateTime.Now;
                return true;
            }
            if (LastExecuted.Value == DateTime.Now.AddMinutes(Interval))
            {
                LastExecuted = DateTime.Now;
                return true;
            }
            return false;
        }
    }
}