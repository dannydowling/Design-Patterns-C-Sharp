using System;
using System.Collections.Generic;
using System.Linq;

namespace DesignPatterns.Extensions
{
    public static class DateTimeExtension
    {
        public static List<DateTime> SortAscending(this List<DateTime> list)
        {
            // Arrange a list of DateTimes chronologically
            list.Sort((a, b) => a.CompareTo(b));
            return list;
        }

        public static List<DateTime> SortDescending(this List<DateTime> list)
        {
            // Arrange a list of DateTimes in Reverse Chronology
            list.Sort((a, b) => b.CompareTo(a));
            return list;
        }

        public static List<DateTime> SortMonthAscending(this List<DateTime> list)
        {
            // Sort a list of DateTimes by their month only
            list.Sort((a, b) => a.Month.CompareTo(b.Month));
            return list;
        }

        public static List<DateTime> SortMonthDescending(this List<DateTime> list)
        {
            // sort a list of DateTimes by their month in reverse order
            list.Sort((a, b) => b.Month.CompareTo(a.Month));
            return list;
        }

        public static DateTime EarliestDateTime(this List<DateTime> list)
        {
            // sort Ascending, then take the first element in the list
            list.Sort((a, b) => a.CompareTo(b));
            return list[0];
        }

        public static DateTime OldestDateTime(this List<DateTime> list)
        {
            // sort Chronologically
            list.Sort((a, b) => a.CompareTo(b));

            // 0 is the first element in the list,
            // so we have to take 1 away from the count of them
            return list[list.Count - 1];
        }

        public static DateTime MostRecentFutureDateTime(this List<DateTime> list)
        {
            // Finds the DateTime closest to the current Time, in a list of future DateTimes.

            DateTime baseTime = DateTime.Now;
            list.Add(baseTime);
            // sort chronologically
            list.Sort((a, b) => a.CompareTo(b));

            DateTime result = new DateTime();

            // start from now, going to the end of the list, incrementing in 1 second increments.
            for (DateTime i = baseTime; i < list.OldestDateTime(); TimeSpan.FromSeconds(1))
            {
                // using Linq to find the index position in the sorted list and from there return 
                // the first element in the continued sequence.
                result = list.Where(x => x.Equals(i)).First();
            }
            return result;
        }

        public static DateTime MostRecentPastDateTime(this List<DateTime> list)
        {
            // Finds the DateTime closest to the current Time, in a list of past DateTimes

            DateTime baseTime = DateTime.Now;
            list.Add(baseTime);
            list.Sort((a, b) => a.CompareTo(b));

            DateTime result = new DateTime();

            // same as above, but decrementing with the indexer.
            for (DateTime i = baseTime; i < list.EarliestDateTime(); TimeSpan.FromSeconds(-1))
            {
                result = list.Where(x => x.Equals(i)).First();
            }
            return result;
        }

       
    }
}

