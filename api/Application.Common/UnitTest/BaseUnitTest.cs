namespace App.Common.UnitTest
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    public abstract class BaseUnitTest
    {
        public App.Common.Application.IApplication Application { get; protected set; }
        public BaseUnitTest()
        {
            this.Application = App.Common.Application.ApplicationFactory.Create<System.Web.HttpApplication>(App.Common.ApplicationType.UnitTest, null);
        }

        [TestInitialize()]
        public void Init()
        {
            this.OnInit();
        }

        protected virtual void OnInit()
        {
            this.Application.OnApplicationStarted();
        }

        [TestCleanup()]
        public void Finished()
        {
            this.OnFinished();
        }

        protected virtual void OnFinished()
        {
            this.Application.OnApplicationEnded();
        }
    }
}
