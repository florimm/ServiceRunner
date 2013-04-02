using System;

namespace Contracts
{
    public class OneTimeScheduler : Scheduler
    {
        public override ExecuteType ExecuteType
        {
            get
            {
                return ExecuteType.OneTime;
            }
        }
        public override bool CanBeExecuted()
        {
            if (!LastExecuted.HasValue)
            {
                LastExecuted = DateTime.Now;
                return true;
            }
            return false;
        }
    }
}