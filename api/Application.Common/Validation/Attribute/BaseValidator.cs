namespace App.Common.Validation.Attribute
{
    using System;

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class BaseAttribute : System.Attribute
    {
        public string MessageKey { get; set; }
        public BaseAttribute(string key)
        {
            this.MessageKey = key;
        }

        public virtual bool IsValid(ValidationRequest validateRequest)
        {
            return true;
        }
    }
}