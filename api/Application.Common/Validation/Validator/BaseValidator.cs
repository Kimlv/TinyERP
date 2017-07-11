namespace App.Common.Validation.Validator
{
    using System;
    using System.Collections.Generic;

    public class BaseValidator : IValidator
    {
        public virtual bool IsMatch(object value, object evaluator)
        {
            throw new NotImplementedException();
        }

        public virtual bool IsRequire(object value)
        {
            return value != null;
        }

        public virtual bool IsValueInRange(object value, object lowerBound, object upperBound)
        {
            throw new NotImplementedException();
        }

        public virtual bool IsValueInCollection(object value, IList<object> values)
        {
            throw new NotImplementedException();
        }

        public bool IsUnique(object value, object options)
        {
            throw new NotImplementedException();
        }
    }
}