using System;

namespace BeEmote.Common
{
    public static class Formatter
    {
        public static string Percent(double? value)
        {
            return value is null
                ? "-"
                : $"{Math.Round((double)value * 100, 2)}%";
        }
    }
}
