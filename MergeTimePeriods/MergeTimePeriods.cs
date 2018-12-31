namespace MergeTimePeriods
{
    using System.Collections.Generic;
    using System.Linq;

    public static class MergeTimePeriods
    {
        public static List<TimePeriod> Merge(List<TimePeriod> timePeriods)
        {
            var result = new HashSet<TimePeriod>();

            foreach (var period in timePeriods)
            {
                var smallPeriod = result.FirstOrDefault(p => p.StartDate.Ticks <= period.StartDate.Ticks &&
                                                p.EndDate.Ticks >= period.EndDate.Ticks &&
                                                p.StartDate.Ticks <= period.EndDate.Ticks &&
                                                p.EndDate.Ticks >= period.StartDate.Ticks);
                if (smallPeriod != null)
                {
                    continue;
                }

                var largePeriod = result.FirstOrDefault(p => p.StartDate.Ticks >= period.StartDate.Ticks &&
                                                             p.EndDate.Ticks <= period.EndDate.Ticks &&
                                                             p.StartDate.Ticks <= period.EndDate.Ticks &&
                                                             p.EndDate.Ticks >= period.StartDate.Ticks);
                if (largePeriod != null)
                {
                    largePeriod.StartDate = period.StartDate;
                    largePeriod.EndDate = period.EndDate;
                    continue;
                }

                var periodToMerge = result.FirstOrDefault(p =>
                            (p.StartDate.Ticks <= period.StartDate.Ticks &&
                             p.EndDate.Ticks >= period.StartDate.Ticks &&
                                p.EndDate.Ticks <= period.EndDate.Ticks) ||
                            (p.StartDate.Ticks >= period.StartDate.Ticks &&
                             p.StartDate.Ticks <= period.EndDate.Ticks &&
                                p.EndDate.Ticks >= period.EndDate.Ticks));

                if (periodToMerge != null)
                {
                    if (period.StartDate < periodToMerge.StartDate)
                    {
                        periodToMerge.StartDate = period.StartDate;
                    }
                    else
                    {
                        periodToMerge.EndDate = period.EndDate;
                    }
                }
                else
                {
                    result.Add(period);
                }
            }

            return result.ToList();
        }
    }
}