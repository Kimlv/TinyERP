namespace App.Common.Command.Exceptions
{
    public class CommandHandlerNotFound<TCommand> : CommandHandlerException<TCommand>
    {
        public CommandHandlerNotFound() : base(CommandHandlerExceptionKeys.CommandHandlerNotFound){}
    }
}