namespace App.Common.UnitTest
{
    using Configurations;
    using Connector;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    public abstract class BaseIntegrationTest
    {
        public App.Common.Application.IApplication Application { get; protected set; }
        protected string BaseUrl { get; private set; }
        protected IConnector Connector { get; private set; }
        public BaseIntegrationTest(string uri)
        {
            this.Application = App.Common.Application.ApplicationFactory.Create<System.Web.HttpApplication>(App.Common.ApplicationType.UnitTest, null);
            this.BaseUrl = uri;
            this.Connector = ConnectorFactory.Create(ConnectorType.REST);
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
