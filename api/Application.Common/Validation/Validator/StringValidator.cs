namespace App.Common.Validation.Validator
{
    using System.Collections.Generic;
    using App.Common.Helpers;
    using System.Linq;

    public class StringValidator : BaseValidator
    {
        public override bool IsRequire(object value)
        {
            return value != null && (value is string) && !string.IsNullOrWhiteSpace((string)value);
        }

        public override bool IsValueInRange(object value, object lowerBound, object upperBound)
        {
            string str = value as string;
            int valueLength;
            int low = (int)lowerBound;
            int upper = (int)upperBound;
            valueLength = string.IsNullOrWhiteSpace(str) ? 0 : str.Length;
            return valueLength >= low && valueLength <= upper;
        }

        public override bool IsMatch(object value, object evaluator)
        {
            string pattern = evaluator as string;
            string val = value as string;
            return RegexHelper.IsMatch(pattern, val);
        }

        public override bool IsValueInCollection(object value, IList<object> values)
        {
            string val = value as string;
            IList<string> vals = values.Cast<string>().ToList();
            return vals.Any(item => item.Equals(val, System.StringComparison.CurrentCultureIgnoreCase));
        }
    }
}
