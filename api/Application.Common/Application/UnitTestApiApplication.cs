namespace App.Common.Application
{
    public class UnitTestApplication<TContext> : BaseApplication<TContext>
    {
        public UnitTestApplication(TContext context) : base(context, ApplicationType.Console)
        {
        }
    }
}