namespace App.Common.Helpers
{
    using System;
    using App.Common.Configurations;
    using App.Common.Logging;
    using App.Common.DI;

    public class DateTimeHelper
    {
        public static bool IsExpired(System.DateTime utcValueToCompare)
        {
            DateTime currentUtc = GetUtcDateTime();
            bool result = DateTime.Compare(currentUtc, utcValueToCompare) >= 0;
            ILogger logger = IoC.Container.Resolve<ILogger>();
            logger.Info("TIme:{0}, current :{1}, result:{2}", utcValueToCompare, currentUtc, result);
            return result;
        }

        private static DateTime GetUtcDateTime()
        {
            return DateTime.UtcNow;
        }

        public static DateTime GetAuthenticationTokenExpiredUtcDateTime()
        {
            return DateTime.UtcNow.AddMinutes(Configuration.Current.Authentication.TokenExpiredAfterInMinute);
        }
    }
}