namespace App.Common.Command
{
    public interface ICommandHandlerStrategy
    {
        void Execute<TCommand>(TCommand command) where TCommand : IBaseCommand;
    }
}
