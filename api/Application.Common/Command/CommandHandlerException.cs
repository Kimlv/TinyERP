namespace App.Common.Command
{
    using App.Common.Validation;
    public class CommandHandlerException<TCommand> : ValidationException
    {
        public CommandHandlerException(string key) : base(key) { }
    }
}
