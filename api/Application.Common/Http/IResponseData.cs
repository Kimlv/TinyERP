namespace App.Common.Http
{
    using System.Collections.Generic;
    using App.Common.Validation;
    using System.Net;

    public interface IResponseData<DataType>
    {
        DataType Data { get; set; }
        IList<ValidationError> Errors { get; set; }
        HttpStatusCode Status { get; set; }
        void SetStatus(System.Net.HttpStatusCode httpStatusCode);
        void SetErrors(IList<ValidationError> errors);
        void SetData(DataType data);
    }
}