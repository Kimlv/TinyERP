namespace App.Common.UITest.UI
{
    using App.Common.Configurations;
    using System.Xml;

    public class UINavigateAction : BaseUIAction
    {
        public string Url { get; set; }
        public string Timeout { get; set; }
        public UINavigateAction(XmlNode node, App.Common.UITest.Runner.IWebDriver webDriver)
            : base(node, UIActionType.Input, webDriver)
        {
            this.Url = node.Attributes["url"].Value;
            this.Timeout = node.Attributes["timeout"] != null ? node.Attributes["timeout"].Value : Configuration.Current.UITest.Timeout.ToString();
        }

        public override void ResolveParams(System.Collections.Generic.IList<Suite.TestDataKeyNamePair> actionParams)
        {
            base.ResolveParams(actionParams);
            this.Timeout = this.RepalceParamValue(this.Timeout, actionParams);
            this.Url = this.RepalceParamValue(this.Url, actionParams);
        }

        public override void Execute()
        {
            base.Execute();
            this.WebDriver.Navigate(this);
        }
    }
}