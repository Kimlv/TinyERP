namespace App.Common.Tasks
{
    using System.Web;

    public interface IApplicationEndedTask<TContext> : IBaseTask<TContext>
    {
    }
}