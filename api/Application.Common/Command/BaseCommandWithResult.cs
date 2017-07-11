namespace App.Common.Command
{
    public class BaseCommandWithResult<TResult> : BaseCommand
    {
        public TResult Result { get; set; }
        public BaseCommandWithResult() : base() { }
    }
}
