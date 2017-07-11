namespace App.Common.Command
{
    public interface IBaseCommandHandler<TCommand> where TCommand : IBaseCommand
    {
        void Handle(TCommand command);
    }
}
