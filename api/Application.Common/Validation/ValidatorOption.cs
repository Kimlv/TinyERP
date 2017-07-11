namespace App.Common.Validation
{
    public class ValidatorOption
    {
        public DataOperationType Operation { get; set; }
        public string Field { get; set; }
        public ValidatorOption()
        {
            this.Operation = DataOperationType.None;
        }

        public ValidatorOption(DataOperationType operation, string field)
        {
            this.Operation = operation;
            this.Field = field;
        }
    }
}
