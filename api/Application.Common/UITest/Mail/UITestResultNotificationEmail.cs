namespace App.Common.UITest.Mail
{
    using App.Common.Configurations;
    using App.Common.Helpers;
    using App.Common.Mail;

    public class UITestResultNotificationEmail : EmailContent
    {
        public UITestResultNotificationEmail(string zipFile) : base()
        {
            this.Subject = ResourceHelper.ToResourceKey("UITest.NotificationEmailTitle");
            this.Body = ResourceHelper.ToResourceKey("uitest\\uitesttesultnotificationemail.html", ResourceType.MailTemplate);
            this.To = Configuration.Current.UITest.NotificationReceiver;
            this.Attachments.Add(zipFile);
        }
    }
}