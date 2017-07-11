namespace App.MessageBus.Controllers
{
    using Common.MessageBus;
    using Common.MVC.Attributes;
    using System.Web.Http;
    using Common.Command;
    using MessageBus.Aggregate;
    using MessageBus.CommandHandler.BusEvent;

    [RoutePrefix("api/busevents")]
    public class MessageBusEventCommandHandler : CommandHandlerController<BusEventAggregate>
    {
        [HttpPost]
        [Route("")]
        [ResponseWrapper()]
        public CreateMessageBusEventResponse CreateMessage(CreateBusEventRequest ev)
        {
            this.Execute(ev);
            /// need to consider how to return response data to caller
            return new CreateMessageBusEventResponse();
        }
    }
}