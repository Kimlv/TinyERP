using System;

namespace App.Common.Extensions
{
    public static class EnumExtension
    {
        public static bool IsIncludedIn(this ApplicationType first, ApplicationType second)
        {
            return (first & second) != 0;
        }

        public static bool IsIncludedIn(this UserRole first, UserRole second)
        {
            return (first & second) != 0;
        }

        public static bool IsIncludedIn(this AuthType first, AuthType second)
        {
            return (first & second) != 0;
        }
    }
}