namespace App.Security.Event
{
    using App.Common.Event;
    using System;

    public class OnUserCreated : BaseEvent
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }

        public OnUserCreated() : base(Common.EventPriority.High){}
    }
}
