namespace App.Common.UITest.Suite
{
    using App.Common.Configurations;
    using App.Common.Helpers;
    using App.Common.UITest.Runner;
    using System.Collections.Generic;
    using System.IO;
    using App.Common.Extensions;

    public class TestCase : BaseExecutable
    {
        public TestCaseRef TestCaseRef { get; protected set; }
        public TestCaseCollection TestCaseCollection { get; protected set; }
        public IWebDriver WebDriver { get; protected set; }
        public TestCase(TestCaseRef tcf, TestCaseCollection testCaseCollection)
        {
            this.TestCaseRef = tcf;
            this.TestCaseCollection = testCaseCollection;
        }

        public override void Execute()
        {
            IList<TestCaseAction> actions = this.Execute(this.TestCaseRef);
            this.PrintReport(this.TestCaseRef, actions);
        }

        public override void BeforeExecute()
        {
            base.BeforeExecute();
            this.WebDriver = this.TestCaseCollection.Suite.TestRunner.CreateWebDriver();
        }

        public override void AfterExecute()
        {
            base.AfterExecute();
            this.WebDriver.Dispose();
        }

        private void PrintReport(Suite.TestCaseRef tcref, IList<TestCaseAction> actions)
        {
            this.TestCaseCollection.Suite.TestRunner.Writer.Write(tcref, actions);
            foreach (TestCaseAction action in actions)
            {
                this.TestCaseCollection.Suite.TestRunner.Writer.Write(action);
            }
        }

        private IList<TestCaseAction> Execute(Suite.TestCaseRef tcref)
        {
            IList<TestCaseAction> actions = new List<TestCaseAction>();
            IList<TestDataKeyNamePair> testData = this.GetTestData(tcref.Key);
            foreach (TestCaseActionRef actionRef in tcref.Actions)
            {
                TestCaseAction testAction = new TestCaseAction(actionRef, this, testData, this.TestCaseCollection.Includes);
                if (actionRef.SleepBefore.AsInt() > 0)
                {
                    this.WebDriver.Sleep(actionRef.SleepBefore.AsInt());
                }

                testAction.Execute();
                actions.Add(testAction);
                if (testAction.Status == TestResultType.Fail)
                {
                    tcref.Status = TestResultType.Fail;
                    break;
                }
            }

            tcref.Status = TestResultType.Success;
            return actions;
        }

        private IList<TestDataKeyNamePair> GetTestData(string testCaseKey)
        {
            string dataFilePath = Path.Combine(Configuration.Current.UITest.BasePath, this.TestCaseCollection.Suite.SuiteRef.DataFile);
            TestCaseData data = XmlHelper.LoadNodeAsObject<TestCaseData>(dataFilePath, @"/testcases/testcase[@key='" + testCaseKey + "']");
            IList<TestDataKeyNamePair> keyPairs = data.Values;
            TestCaseHelper.AddEnvirontmentParams(keyPairs, this.WebDriver.TestRunner.Environment);
            return keyPairs;
        }
    }
}