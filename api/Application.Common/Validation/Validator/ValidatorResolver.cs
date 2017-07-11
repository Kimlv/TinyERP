namespace App.Common.Validation.Validator
{
    public class ValidatorResolver
    {
        public static IValidator Resolve(string type)
        {
            DataType dataType = GetDataType(type);
            switch (dataType)
            {
                case DataType.Guid:
                    return new GuidValidator();
                case DataType.String:
                    return new StringValidator();
                default:
                    return new BaseValidator();
            }
        }

        private static DataType GetDataType(string type)
        {
            switch (type.ToLower())
            {
                case "system.guid":
                    return DataType.Guid;
                case "system.string":
                    return DataType.String;
                default:
                    return DataType.Object;
            }
        }
    }
}