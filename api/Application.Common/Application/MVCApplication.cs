namespace App.Common.Application
{
    public class MVCApplication<TContext> : BaseApplication<TContext>
    {
        public MVCApplication(TContext context) : base(context, ApplicationType.MVC)
        {
        }
    }
}