namespace App.Common.Validation
{
    using System.Collections.Generic;

    public interface IValidationException
    {
        IList<ValidationError> Errors { get; set; }
        void Add(ValidationError error);
        void ThrowIfError();
        bool HasExceptionKey(string key);
    }
}