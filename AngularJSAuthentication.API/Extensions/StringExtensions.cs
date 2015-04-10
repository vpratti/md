using System;

namespace AngularJSAuthentication.API.Extensions
{
    public static class StringExtensions
    {
        public static bool EqualsIgnoreCase(this string val, string compare)
        {
            return String.Equals(val, compare, StringComparison.OrdinalIgnoreCase);
        }
    }
}