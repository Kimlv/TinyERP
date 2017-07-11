namespace App.Common.Validation.Validator
{
    using System;

    public class GuidValidator : BaseValidator
    {
        public override bool IsRequire(object value)
        {
            if (!(value is Guid)) { return false; }
            Guid val = (Guid)value;
            return val != Guid.Empty;
        }
    }
}
