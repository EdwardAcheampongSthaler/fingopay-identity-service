using System;

namespace STH.BiometricIdentityService.Infrastructure.Extensions
{
    public static class DateExtensions
    {
        public static string ToHGSDateFormat(this DateTime? date)
        {
            return date.HasValue ? date.GetValueOrDefault().ToString("yyyy-MM-dd") : string.Empty;
        }
        public static string ToHGSDateTimeFormat(this DateTime? date)
        {
            return date.HasValue ? date.GetValueOrDefault().ToString("yyyy-MM-dd hh:mm:ss.fff") : string.Empty;
        }
    }
}