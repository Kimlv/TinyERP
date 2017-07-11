namespace App.Security.Event.User
{
    using App.Common.Event;
    using System;

    public class OnUserRoleAdded : BaseEvent
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
    }
}
