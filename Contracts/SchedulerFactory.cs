namespace Contracts
{
    public static class SchedulerFactory
    {
        public static Scheduler Get(ExecuteType type)
        {
            switch (type)
            {
                case ExecuteType.Month:
                    return new MonthScheduler();
                case ExecuteType.Hour:
                    return new HourScheduler();
                case ExecuteType.Min:
                    return new MinScheduler();
                case ExecuteType.OneTime:
                    return new OneTimeScheduler();
            }
            return new DayScheduler();
        }
    }

    public enum ExecuteType
    {
        Min,
        Hour,
        Day,
        Month,
        OneTime
    }
}