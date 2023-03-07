using System;
using System.Collections.Generic;
using System.Text;

namespace Cgpe.Du.Domain.Entities
{
    public static class DateTimeExtensions
    {
        public static DateTime GetMadridLocalDateTime(this DateTime dt)
        {
            DateTime utcTime = DateTime.UtcNow;
            TimeZoneInfo madridTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Romance Standard Time");
            DateTime madridLocalTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, madridTimeZone);
            return madridLocalTime;
        }

        public static DateTime GetMadridLocalDate(this DateTime dt)
        {
            return GetMadridLocalDateTime(dt).Date;
        }
    }
}
