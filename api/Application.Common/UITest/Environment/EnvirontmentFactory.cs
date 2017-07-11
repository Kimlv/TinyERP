namespace App.Common.UITest.Environment
{
    using App.Common.Helpers;

    public class EnvironmentFactory
    {
        public static System.Collections.Generic.IList<IEnvironment> Load(string environtmentFilePath)
        {
            IEnvironment[] items = XmlHelper.Load<Environment[]>(environtmentFilePath, "environments");
            return items;
        }
    }
}