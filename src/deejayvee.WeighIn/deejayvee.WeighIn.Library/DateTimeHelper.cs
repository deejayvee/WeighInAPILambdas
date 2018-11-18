using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace deejayvee.WeighIn.Library
{
    public static class DateTimeHelper
    {
        private static readonly string SYDNEY_TIMEZONE1 = "Australia/Sydney";
        private static readonly string SYDNEY_TIMEZONE2 = "AUS Eastern Standard Time";


        internal static DateTime Sydney(this DateTime date)
        {
            TimeZoneInfo SydneyTimezone;
            if (TimeZoneInfo.GetSystemTimeZones().Any(TZ => TZ.Id == SYDNEY_TIMEZONE1))
            {
                SydneyTimezone = TimeZoneInfo.FindSystemTimeZoneById(SYDNEY_TIMEZONE1);
            }
            else if (TimeZoneInfo.GetSystemTimeZones().Any(TZ => TZ.Id == SYDNEY_TIMEZONE2))
            {
                SydneyTimezone = TimeZoneInfo.FindSystemTimeZoneById(SYDNEY_TIMEZONE2);
            }
            else
            {
                SydneyTimezone = TimeZoneInfo.Local;
            }

            return TimeZoneInfo.ConvertTime(date, SydneyTimezone);
        }

        public static DateTime SydneyNow
        {
            get
            {
                return DateTime.Now.Sydney();
            }
        }

        public static DateTime SydneyToday
        {
            get
            {
                return DateTime.Now.Sydney().Date;
            }
        }

    }
}
