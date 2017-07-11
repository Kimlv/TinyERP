namespace App.Common.Validation.Attribute
{
    using App.Common.Validation.Validator;
    using System.Collections.Generic;
    using System.Linq;

    public class ValueInCollectionAttribute : BaseAttribute
    {
        public IList<object> Values { get; set; }
        public ValueInCollectionAttribute(string key, object[] values) : base(key)
        {
            this.Values = values.ToList();
        }

        public override bool IsValid(ValidationRequest validateRequest)
        {
            IValidator validator = ValidatorResolver.Resolve(validateRequest.DataType);
            return validator.IsValueInCollection(validateRequest.Value, this.Values);
        }
    }
}
