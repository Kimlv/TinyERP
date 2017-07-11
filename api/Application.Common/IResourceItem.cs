namespace App.Common
{
    using System.Collections.Generic;

    public interface IResourceItem
    {
        string Key { get; set; }
        string Message { get; set; }
        IList<string> Params { get; set; }
    }
}