namespace App.MessageBus.CommandHandler.Impl.BusEvent
{
    using App.MessageBus.CommandHandler.BusEvent;
    using Common.Command;
    using Aggregate;
    using Common.Data;
    using Common.Aggregate;
    using Common.DI;
    using System;
    using Common.Validation;
    using Common.Helpers;

    internal class BusEventCommandHandler : BaseCommandHandler, IBusEventCommandHandler
    {
        public void Handle(CreateBusEventRequest command)
        {
            this.ValidateCreateBusEventRequest(command);
            BusEventAggregate aggregate = AggregateFactory.Create<BusEventAggregate>();
            aggregate.CreateEventContent(command.Key, command.Content);
            using (IUnitOfWork uow = this.CreateUnitOfWork<BusEventAggregate>())
            {
                Repository.IBusEventRepository repo = IoC.Container.Resolve<Repository.IBusEventRepository>(uow);
                repo.Add(aggregate);
                uow.Commit();
                aggregate.PublishEvents();
            }
        }

        private void ValidateCreateBusEventRequest(CreateBusEventRequest command)
        {
            IValidationException validation = ValidationHelper.Validate(command);
            validation.ThrowIfError();
        }
    }
}
