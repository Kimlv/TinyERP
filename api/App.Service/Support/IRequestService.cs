namespace App.Service.Support
{
    using System;
    using System.Collections.Generic;

    public interface IRequestService
    {
        void CreateRequest(CreateRequest request);
        IList<SupportRequestListItem> GetRequests();
        GetRequestResponse GetRequest(Guid itemId);
        void MarkAsResolved(Guid itemId);
        void Cancel(Guid itemId);
    }
}
