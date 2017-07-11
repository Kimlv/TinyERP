namespace App.Common.Helpers
{
    using App.Common.Validation;
    using App.Common.Validation.Attribute;

    public class ValidationHelper
    {
        public static IValidationException Validate(object obj)
        {
            IValidationException ex = new ValidationException();
            if (obj == null)
            {
                ex.Add(new ValidationError("common.error.objectIsNull"));
                return ex;
            }

            foreach (ValidationRequest request in ObjectHelper.GetPropertyAttributes<BaseAttribute>(obj))
            {
                if (request.Validator == null || request.Validator.IsValid(request)) { continue; }
                ex.Add(new ValidationError(request.Validator.MessageKey));
            }

            return ex;
        }
    }
}