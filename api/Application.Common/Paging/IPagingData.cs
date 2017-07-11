namespace App.Common.Paging
{
    using System.Collections.Generic;

    public interface IPagingData<DataType>
    {
        IList<DataType> Items { get; }
        int PageIndex { get; }
        int PageSize { get; }
        int TotalItems { get; }
        int TotalPages { get; }
    }
}