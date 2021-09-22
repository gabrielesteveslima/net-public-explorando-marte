using System;

namespace NASA.Explorer.Console.Utils
{
    public static class Converters
    {
        public static T ConvertToEnum<T>(this string value)
        {
            return (T) Enum.Parse(typeof(T), value);
        }

        public static int ConvertToInt(this string value)
        {
            return Convert.ToInt32(value);
        }
    }
}