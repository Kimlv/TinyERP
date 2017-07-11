namespace App.Common.UITest.Runner
{
    using App.Common.UITest.Environment;

    public class TestRunnerFactory
    {
        public static ITestRunner Create(IEnvironment environment)
        {
            return new App.Common.UITest.Runner.Selenium.SeleniumTestRunner(environment);
        }
    }
}