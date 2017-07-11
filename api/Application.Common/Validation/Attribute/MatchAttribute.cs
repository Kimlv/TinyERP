namespace App.Common.Validation.Attribute
{
    using App.Common.Validation.Validator;

    public class MatchAttribute : BaseAttribute
    {
        public object Evaluator { get; protected set; }
        public MatchAttribute(string key, object evaluator) : base(key)
        {
            this.Evaluator = evaluator;
        }

        public override bool IsValid(ValidationRequest validateRequest)
        {
            IValidator validator = ValidatorResolver.Resolve(validateRequest.DataType);
            return validator.IsMatch(validateRequest.Value, this.Evaluator);
        }
    }
}