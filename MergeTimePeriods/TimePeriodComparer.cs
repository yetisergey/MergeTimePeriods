namespace MergeTimePeriods
{
    using System.Collections.Generic;

    public class TimePeriodComparer : IEqualityComparer<TimePeriod>
    {
        public bool Equals(TimePeriod x, TimePeriod y)
        {
            return x.StartDate == y.StartDate && x.EndDate == y.EndDate;
        }

        public int GetHashCode(TimePeriod obj)
        {
            return obj.EndDate.GetHashCode() - obj.StartDate.GetHashCode();
        }
    }
}