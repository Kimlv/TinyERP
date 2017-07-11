namespace App.Common.Helpers
{
    public class EmailHelper
    {
        public static bool IsValid(string email)
        {
            return !string.IsNullOrWhiteSpace(email);
        }
    }
}