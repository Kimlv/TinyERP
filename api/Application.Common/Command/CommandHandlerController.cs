namespace App.Common.Command
{
    using Aggregate;
    using MVC;

    public class CommandHandlerController<TAggregate> : BaseApiController where TAggregate : IBaseAggregateRoot
    {
        private ICommandHandlerStrategy commandHandlerStrategy;
        public CommandHandlerController() : base()
        {
            this.commandHandlerStrategy = CommandHandlerStrategyFactory.Create<TAggregate>();
        }
        protected void Execute<TCommand>(TCommand command) where TCommand : IBaseCommand
        {
            this.commandHandlerStrategy.Execute<TCommand>(command);
            //IBaseCommandHandler<TCommand> handler = IoC.Container.Resolve<IBaseCommandHandler<TCommand>>();
            //if (handler == null)
            //{
            //    throw new CommandHandlerNotFound<TCommand>();
            //}
            //handler.Handle(command);
        }
    }
}
