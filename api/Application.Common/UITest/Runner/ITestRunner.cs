namespace App.Common.UITest.Runner
{
    using App.Common.UITest.Environment;

    public interface ITestRunner : System.IDisposable
    {
        App.Common.UITest.Writer.ITestWriter Writer { get; }
        IEnvironment Environment { get; }
        IWebDriver CreateWebDriver();
    }
}