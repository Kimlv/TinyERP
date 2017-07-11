namespace App.Common.Validation.Attribute
{
    using App.Common.Validation.Validator;

    public class RequiredAttribute : BaseAttribute
    {
        public RequiredAttribute(string key) : base(key)
        {
        }

        public override bool IsValid(ValidationRequest validateRequest)
        {
            IValidator validator = ValidatorResolver.Resolve(validateRequest.DataType);
            return validator.IsRequire(validateRequest.Value);
        }
    }
}