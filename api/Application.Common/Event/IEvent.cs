namespace App.Common.Event
{
    using System;
    public interface IEvent
    {
        Type HandlerType { get;}
        EventPriority Priority { get; set; }
    }
}