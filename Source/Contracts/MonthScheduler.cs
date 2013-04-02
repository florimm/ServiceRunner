using System;

namespace Contracts
{
    public class MonthScheduler : Scheduler
    {
        public override ExecuteType ExecuteType
        {
            get
            {
                return ExecuteType.Month;
            }
        }
        public override bool CanBeExecuted()
        {
            if (!LastExecuted.HasValue)
            {
                LastExecuted = DateTime.Now;
                return true;
            }
            if (LastExecuted.Value == DateTime.Now.AddMonths(Interval))
            {
                LastExecuted = DateTime.Now;
                return true;
            }
            return false;
        }
    }
}