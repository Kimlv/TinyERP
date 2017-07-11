namespace App.Common.Connector
{
    using App.Common.Http;

    public interface IConnector
    {
        IResponseData<TResponse> Delete<TResponse>(string uri);
        IResponseData<TResponse> Post<TRequest, TResponse>(string uri, TRequest data);
        IResponseData<TResponse> Put<TRequest, TResponse>(string uri, TRequest data);
        IResponseData<TResponse> Get<TResponse>(string uri);
    }
}
