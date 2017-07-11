namespace App.Common.UITest.Writer
{
    using App.Common.UITest.Suite;
    using App.Common.UITest.UI;
    using Environment;
    using System;
    using System.Collections.Generic;

    public interface ITestWriter : IDisposable
    {
        void Write(Suite.ITestSuite testSuite);
        void Write(Suite.TestCaseCollection testCaseCollection);
        void Write(Suite.TestCaseRef testCaseRef, IList<TestCaseAction> actions);
        void Write(IEnvironment environment);
        void Write(Suite.TestCaseAction action);
        void Write(IUIAction uiAction);
    }
}