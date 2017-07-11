namespace App.Service.Impl.Support
{
    using System;
    using App.Service.Support;
    using App.Common.Data;
    using App.Context;
    using App.Common;
    using App.Common.DI;
    using App.Common.Validation;
    using App.Repository.Support;
    using App.Entity.Support;
    using System.Collections.Generic;
    using App.Common.Event;
    using App.EventHandler.Support;

    internal class RequestService : IRequestService
    {
        public void Cancel(Guid itemId)
        {
            SupportRequestOnStatusChanged ev = this.SetRequestStatus(itemId, ItemStatus.Cancelled);
            IEventManager eventManager = IoC.Container.Resolve<IEventManager>();
            eventManager.Publish(ev);
        }

        private SupportRequestOnStatusChanged SetRequestStatus(Guid itemId, ItemStatus status)
        {
            SupportRequestOnStatusChanged ev;
            this.ValidateSetRequestStatus(itemId);
            using (IUnitOfWork uow = new UnitOfWork(RepositoryType.MSSQL))
            {
                IRequestRepository repo = IoC.Container.Resolve<IRequestRepository>(uow);
                Request request = repo.GetById(itemId.ToString());
                ev = new SupportRequestOnStatusChanged(request.Id, request.Subject, request.Status, status, request.Email);
                request.Status = status;
                repo.Update(request);
                uow.Commit();
            }

            return ev;
        }

        private void ValidateSetRequestStatus(Guid itemId)
        {
            IRequestRepository repo = IoC.Container.Resolve<IRequestRepository>();
            if (repo.GetById(itemId.ToString()) == null)
            {
                throw new ValidationException("support.viewRequest.updateStatus.invalidRequest");
            }
        }

        public void MarkAsResolved(Guid itemId)
        {
            SupportRequestOnStatusChanged ev = this.SetRequestStatus(itemId, ItemStatus.Resolved);
            IEventManager eventManager = IoC.Container.Resolve<IEventManager>();
            eventManager.Publish(ev);
        }

        public void CreateRequest(CreateRequest request)
        {
            this.ValidateCreateRequest(request);
            using (IUnitOfWork uow = new UnitOfWork(RepositoryType.MSSQL))
            {
                IRequestRepository repo = IoC.Container.Resolve<IRequestRepository>(uow);
                Request item = new Request(request.Subject, request.Description, request.Email);
                repo.Add(item);
                uow.Commit();
            }
        }

        public GetRequestResponse GetRequest(Guid itemId)
        {
            IRequestRepository repo = IoC.Container.Resolve<IRequestRepository>();
            return repo.GetById<GetRequestResponse>(itemId.ToString());
        }

        public IList<SupportRequestListItem> GetRequests()
        {
            IRequestRepository repo = IoC.Container.Resolve<IRequestRepository>();
            return repo.GetItems<SupportRequestListItem>();
        }

        private void ValidateCreateRequest(CreateRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Subject))
            {
                throw new ValidationException("support.createRequest.validation.subjectIsRequired");
            }

            if (string.IsNullOrWhiteSpace(request.Email))
            {
                throw new ValidationException("support.createRequest.validation.emailIsRequired");
            }
        }
    }
}
