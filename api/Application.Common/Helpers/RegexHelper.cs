namespace App.Common.Helpers
{
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    public class RegexHelper
    {
        public static IList<string> ResolvePattern(string pattern, string value)
        {
            IList<string> result = new List<string>();
            foreach (Match m in GetMatchCollection(pattern, value))
            {
                result.Add(m.Groups.Count >= 2 ? m.Groups[1].Value : m.Value);
            }

            return result;
        }

        public static MatchCollection GetMatchCollection(string pattern, string value)
        {
            Regex regex = new Regex(pattern);
            return regex.Matches(value);
        }

        public static bool IsMatch(string pattern, string value)
        {
            Regex regex = new Regex(pattern);
            return regex.IsMatch(value);
        }
    }
}
