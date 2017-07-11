namespace App.Common.Validation
{
    using System.Collections.Generic;

    public class ValidationError : IResourceItem
    {
        public string Key { get; set; }
        public string Message { get; set; }
        public IList<string> Params { get; set; }
        public ValidationError()
        {
        }

        public ValidationError(string key, string message, IList<string> args = null)
        {
            this.Key = key;
            this.Message = message;
            this.Params = args ?? new List<string>();
        }

        public ValidationError(string key, IList<string> args = null)
        {
            this.Key = key;
            this.Params = args ?? new List<string>();
        }
    }
}