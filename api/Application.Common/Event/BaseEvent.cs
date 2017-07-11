﻿namespace App.Common.Event
{
    using System;
    public class BaseEvent : IEvent
    {
        public virtual Type HandlerType
        {
            get
            {
                return this.GetEventHandler();
            }
        }
        protected virtual Type GetEventHandler()
        {
            Type itype = typeof(IEventHandler<>);
            Type args = this.GetType() ;
            return itype.MakeGenericType(args);
        }
        public EventPriority Priority { get; set; }
        public BaseEvent(EventPriority priority = EventPriority.Normal)
        {
            this.Priority = priority;
        }
    }
}
