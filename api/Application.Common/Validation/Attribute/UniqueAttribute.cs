namespace App.Common.Validation.Attribute
{
    using App.Common.Validation.Validator;
    using System;

    public class UniqueAttribute : BaseAttribute
    {
        public Type EntityType { get; set; }
        public object Options { get; set; }
        public UniqueAttribute(string key, Type type, object options) : base(key)
        {
            this.EntityType = type;
            this.Options = options;
        }

        public override bool IsValid(ValidationRequest validateRequest)
        {
            IValidator validator = ValidatorResolver.Resolve(this.EntityType.FullName);
            return validator.IsUnique(validateRequest.Value, this.Options);
        }
    }
}