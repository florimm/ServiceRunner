using System;

namespace Contracts
{
    public class DayScheduler : Scheduler
    {
        public override ExecuteType ExecuteType
        {
            get
            {
                return ExecuteType.Day;
            }
        }
        public override bool CanBeExecuted()
        {
            if (!LastExecuted.HasValue)
            {
                LastExecuted = DateTime.Now;
                return true;
            }
            if (LastExecuted.Value.Date == DateTime.Now.AddDays(Interval).Date)
            {
                LastExecuted = DateTime.Now;
                return true;
            }
            return false;
        }
    }

}