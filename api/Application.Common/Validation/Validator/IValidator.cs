namespace App.Common.Validation.Validator
{
    using System.Collections.Generic;

    public interface IValidator
    {
        bool IsRequire(object value);
        bool IsValueInRange(object value, object lowerBound, object upperBound);
        bool IsMatch(object value, object evaluator);
        bool IsValueInCollection(object value, IList<object> values);
        bool IsUnique(object value, object options);
    }
}