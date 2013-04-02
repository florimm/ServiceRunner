using System;

namespace Contracts
{
    public class HourScheduler : Scheduler
    {
        public override ExecuteType ExecuteType
        {
            get
            {
                return ExecuteType.Hour;
            }
        }
        public override bool CanBeExecuted()
        {
            if (!LastExecuted.HasValue)
            {
                LastExecuted = DateTime.Now;
                return true;
            }
            if (LastExecuted.Value == DateTime.Now.AddHours(Interval))
            {
                LastExecuted = DateTime.Now;
                return true;
            }
            return false;
        }
    }
}