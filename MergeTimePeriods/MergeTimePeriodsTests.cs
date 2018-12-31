namespace MergeTimePeriods
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    public class MergeTimePeriodsTests
    {
        static readonly DateTime dateTimeNow = DateTime.Now;

        //theory data - List<TimePeriod>
        //excepted data - List<TimePeriod>
        public static TheoryDataCollection<List<TimePeriod>, List<TimePeriod>> timePeriodsTestData = new TheoryDataCollection<List<TimePeriod>, List<TimePeriod>>()
        {
            {
                new List<TimePeriod>()
                {
                    new TimePeriod() { StartDate = dateTimeNow, EndDate = dateTimeNow.AddHours(1) },
                    new TimePeriod() { StartDate = dateTimeNow.AddHours(1), EndDate = dateTimeNow.AddHours(2) }
                },
                new List<TimePeriod>() {
                    new TimePeriod() { StartDate = dateTimeNow, EndDate = dateTimeNow.AddHours(2) }
                }
            },
            {
                new List<TimePeriod>()
                {
                    new TimePeriod() { StartDate = dateTimeNow.AddHours(1), EndDate = dateTimeNow.AddHours(2) },
                    new TimePeriod() { StartDate = dateTimeNow, EndDate = dateTimeNow.AddHours(1) }
                },
                new List<TimePeriod>() {
                    new TimePeriod() { StartDate = dateTimeNow, EndDate = dateTimeNow.AddHours(2) }
                }
            },
            {
                new List<TimePeriod>()
                {
                    new TimePeriod() { StartDate = dateTimeNow.AddHours(1), EndDate = dateTimeNow.AddHours(2) },
                    new TimePeriod() { StartDate = dateTimeNow, EndDate = dateTimeNow.AddHours(1) }
                },
                new List<TimePeriod>() {
                    new TimePeriod() { StartDate = dateTimeNow, EndDate = dateTimeNow.AddHours(2) }
                }
            },
            {
                new List<TimePeriod>()
                {
                    new TimePeriod() { StartDate = dateTimeNow.AddHours(1), EndDate = dateTimeNow.AddHours(2) },
                    new TimePeriod() { StartDate = dateTimeNow, EndDate = dateTimeNow.AddHours(1) },
                    new TimePeriod() { StartDate = dateTimeNow.AddDays(1), EndDate = dateTimeNow.AddDays(1).AddHours(1) }
                },
                new List<TimePeriod>() {
                    new TimePeriod() { StartDate = dateTimeNow, EndDate = dateTimeNow.AddHours(2) },
                    new TimePeriod() { StartDate = dateTimeNow.AddDays(1), EndDate = dateTimeNow.AddDays(1).AddHours(1) }
                }
            },
            {
                Enumerable.Range(1, 100).Select(d=>
                        new TimePeriod() {
                            StartDate = dateTimeNow,
                            EndDate = dateTimeNow.AddYears(d)
                        }
                    ).ToList(),
                new List<TimePeriod>() {
                    new TimePeriod() { StartDate = dateTimeNow, EndDate = dateTimeNow.AddYears(100) },
                }
            },
            {
                Enumerable.Range(1, 100).Select(d=>
                        new TimePeriod() {
                            StartDate = dateTimeNow,
                            EndDate = dateTimeNow.AddTicks(d)
                        }
                    ).ToList(),
                new List<TimePeriod>() {
                    new TimePeriod() { StartDate = dateTimeNow, EndDate = dateTimeNow.AddTicks(100) },
                }
            },
            {
                Enumerable.Range(1, 100).Select(d=>
                        new TimePeriod() {
                            StartDate = dateTimeNow.AddTicks(-d),
                            EndDate = dateTimeNow
                        }
                    ).ToList(),
                new List<TimePeriod>() {
                    new TimePeriod() { StartDate = dateTimeNow.AddTicks(-100), EndDate = dateTimeNow },
                }
            },
            {
                Enumerable.Range(1, 100).Select(d=>
                        new TimePeriod() {
                            StartDate = dateTimeNow.AddYears(-d),
                            EndDate = dateTimeNow
                        }
                    ).ToList(),
                new List<TimePeriod>() {
                    new TimePeriod() { StartDate = dateTimeNow.AddYears(-100), EndDate = dateTimeNow },
                }
            },
            {
                Enumerable.Range(1, 100).Select(d=>
                        new TimePeriod() {
                            StartDate = dateTimeNow.AddMinutes(- d),
                            EndDate = dateTimeNow.AddMinutes(d)
                        }
                    ).ToList(),
                new List<TimePeriod>() {
                    new TimePeriod() { StartDate = dateTimeNow.AddMinutes(-100), EndDate = dateTimeNow.AddMinutes(100) }
                }
            },
            {
                Enumerable.Range(1, 100).Select(d=>
                        new TimePeriod() {
                            StartDate = dateTimeNow.AddYears(-1).AddMinutes(d),
                            EndDate = dateTimeNow.AddYears(1).AddMinutes(- d)
                        }
                    ).ToList(),
                new List<TimePeriod>() {
                    new TimePeriod() { StartDate = dateTimeNow.AddYears(-1).AddMinutes(1), EndDate = dateTimeNow.AddYears(1).AddMinutes(-1) }
                }
            }
        };

        [Theory]
        [MemberData(nameof(timePeriodsTestData))]
        public void Merge_PositiveTests(List<TimePeriod> timePeriods, List<TimePeriod> exceptedTimePeriods)
        {
            var actualResult = MergeTimePeriods.Merge(timePeriods);
            Assert.True(actualResult.SequenceEqual(exceptedTimePeriods, new TimePeriodComparer()));
        }
    }
}