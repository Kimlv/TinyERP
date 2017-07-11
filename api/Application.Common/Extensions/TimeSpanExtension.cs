namespace App.Common.Extensions
{
    using System;

    public static partial class TimeSpanExtension
    {
        public static TimeSpan Value(this TimeSpan? value)
        {
            return value == null ? default(TimeSpan) : value.Value;
        }
    }
}