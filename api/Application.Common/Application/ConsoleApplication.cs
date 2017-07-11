namespace App.Common.Application
{
    public class ConsoleApplication<TContext> : BaseApplication<TContext>
    {
        public ConsoleApplication(TContext context) : base(context, ApplicationType.Console)
        {
        }
    }
}