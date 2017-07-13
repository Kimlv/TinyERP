namespace App.MessageBus.Service.Impl
{
    using App.MessageBus.Service.EventSubcriber;
    using App.Common;
    using App.Common.Data;
    using App.MessageBus.Repository;
    using App.MessageBus.Aggregate;
    using App.Common.Validation;
    using App.Common.Helpers;

    internal class EventSubcriberService : IEventSubcriberService
    {
        public void Register(RegisterEventSubcriber request)
        {
            this.ValidateRegisterEventSubcriberRequest(request);
            using (IUnitOfWork uow = new App.Common.Data.UnitOfWork(RepositoryType.MSSQL))
            {
                IEventSubcriberRepository repo = Common.DI.IoC.Container.Resolve<IEventSubcriberRepository>(uow);
                EventSubcriber subcriber = new EventSubcriber(request.Key, request.Uri, request.ModuleName);
                repo.Add(subcriber);
                uow.Commit();
            }
        }

        private void ValidateRegisterEventSubcriberRequest(RegisterEventSubcriber request)
        {
            IValidationException validationException = ValidationHelper.Validate(request);
            validationException.ThrowIfError();
        }
    }
}
