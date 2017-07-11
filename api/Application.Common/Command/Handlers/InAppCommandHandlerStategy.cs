namespace App.Common.Command.Handlers
{
    using App.Common.Command.Exceptions;
    using DI;

    public class InAppCommandHandlerStategy : ICommandHandlerStrategy
    {
        public void Execute<TCommand>(TCommand command) where TCommand : IBaseCommand
        {
            IBaseCommandHandler<TCommand> handler = IoC.Container.Resolve<IBaseCommandHandler<TCommand>>();
            if (handler == null)
            {
                throw new CommandHandlerNotFound<TCommand>();
            }
            handler.Handle(command);
        }
    }
}
