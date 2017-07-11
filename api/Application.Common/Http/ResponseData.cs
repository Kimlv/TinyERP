namespace App.Common.Http
{
    using System.Collections.Generic;
    using System.Net;
    using AutoMapper.Internal;
    using App.Common.Validation;

    public class ResponseData<DataType> : IResponseData<DataType>
    {
        public HttpStatusCode Status { get; set; }
        public IList<ValidationError> Errors { get; set; }
        public DataType Data { get; set; }
        public ResponseData()
        {
            this.Errors = new List<ValidationError>();
            this.Status = HttpStatusCode.OK;
        }

        public void SetStatus(System.Net.HttpStatusCode httpStatusCode)
        {
            this.Status = httpStatusCode;
        }

        public void SetErrors(IList<ValidationError> errors)
        {
            errors.Each(error => this.Errors.Add(error));
        }

        public void SetData(DataType data)
        {
            this.Data = data;
        }
    }
}