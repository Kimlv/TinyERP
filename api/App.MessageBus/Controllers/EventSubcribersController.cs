namespace App.MessageBus.Controllers
{
    using App.Common.DI;
    using App.Common.Event;
    using App.Common.MVC;
    using App.MessageBus.Service.EventSubcriber;
    using System.Web.Http;
    [RoutePrefix("api/eventsubcribers")]
    public class EventSubcribersController : BaseApiController
    {
        [HttpPost]
        [Route]
        public void Register(RegisterEventSubcriber request)
        {
            IEventSubcriberService service = IoC.Container.Resolve<IEventSubcriberService>();
            service.Register(request);
        }
    }
}