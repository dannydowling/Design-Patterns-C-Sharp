using System;
using System.Collections.Generic;
using System.Linq;

namespace DesignPatterns.Extensions
{
    public static class DateTimeExtension
    {
        public static List<DateTime> SortAscending(this List<DateTime> list)
        {
            list.Sort((a, b) => a.CompareTo(b));
            return list;
        }

        public static List<DateTime> SortDescending(this List<DateTime> list)
        {
            list.Sort((a, b) => b.CompareTo(a));
            return list;
        }

        public static List<DateTime> SortMonthAscending(this List<DateTime> list)
        {
            list.Sort((a, b) => a.Month.CompareTo(b.Month));
            return list;
        }

        public static List<DateTime> SortMonthDescending(this List<DateTime> list)
        {
            list.Sort((a, b) => b.Month.CompareTo(a.Month));
            return list;
        }

        public static DateTime OldestDateTime(this List<DateTime> list)
        {
            //sort Ascending, then take the last one.
            list.Sort((a, b) => a.CompareTo(b));

            return list.Take(list.Count - 1).First();
        }

        public static DateTime MostRecentFutureDateTime(this List<DateTime> list)
        {
            DateTime baseTime = DateTime.Now;
            list.Add(baseTime);
            //sort Ascending
            list.Sort((a, b) => a.CompareTo(b));

            DateTime result = new DateTime();
            for (DateTime i = baseTime; i < list.OldestDateTime(); TimeSpan.FromSeconds(1))
            {
                result = list.Where(x => x.Equals(i)).First();
            }
            return result;
        }

        public static DateTime MostRecentPastDateTime(this List<DateTime> list)
        {
            DateTime baseTime = DateTime.Now;
            list.Add(baseTime);
            //sort Ascending
            list.Sort((a, b) => a.CompareTo(b));

            DateTime result = new DateTime();
            for (DateTime i = baseTime; i < list.EarliestDateTime(); TimeSpan.FromSeconds(-1))
            {
                result = list.Where(x => x.Equals(i)).First();
            }
            return result;
        }

        public static DateTime EarliestDateTime(this List<DateTime> list)
        {
            //sort Descending, then take the last one.
            list.Sort((a, b) => b.CompareTo(a));
            return list.Take(list.Count - 1).First();
        }

    }
}

