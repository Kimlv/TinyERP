namespace App.Common.Validation.Attribute
{
    public class ValidationRequest
    {
        public object Value { get; protected set; }
        public BaseAttribute Validator { get; protected set; }
        public string DataType { get; protected set; }
        public ValidationRequest(BaseAttribute validator, object value, string dataType)
        {
            this.Validator = validator;
            this.Value = value;
            this.DataType = dataType;
        }
    }
}