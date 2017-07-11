namespace App.Common.UITest
{
    using App.Common.Configurations;
    using App.Common.Helpers;
    using App.Common.Mail;
    using App.Common.UITest.Mail;
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class UITestExecutor
    {
        public static void Execute(string environtmentFilePath)
        {
            IList<App.Common.UITest.Environment.IEnvironment> environtments = App.Common.UITest.Environment.EnvironmentFactory.Load(environtmentFilePath);
            foreach (App.Common.UITest.Environment.IEnvironment environtment in environtments)
            {
                environtment.Execute();
            }

            var zipFile = Path.Combine(Configuration.Current.UITest.BaseOutput, string.Format(Configuration.Current.UITest.ZipFile, DateTime.Now.ToString("yyyyMMddHHmm")));
            FileHelper.CreateZipFile(environtments, zipFile);
            UITestExecutor.SendMail(zipFile);
        }

        private static void SendMail(string zipFile)
        {
            UITestResultNotificationEmail emailConent = new UITestResultNotificationEmail(zipFile);
            IMailService mailService = new MailService();
            mailService.Send(emailConent);
        }
    }
}