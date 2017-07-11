namespace App.Security.Command
{
    using App.Common.Command;
    using App.Security.Aggregate;
    using System;

    public class CreateUserCommand : BaseCommandWithResult<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Pwd { get; set; }
        public Roles Roles { get; set; }
        public CreateUserCommand()
        {
            this.Roles = new Roles();
        }
    }
}
