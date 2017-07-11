namespace App.Security.Command
{
    using App.Common.Command;
    public interface ISecurityCommandHandler : IBaseCommandHandler<App.Security.Command.CreateUserCommand>
    {
    }
}
