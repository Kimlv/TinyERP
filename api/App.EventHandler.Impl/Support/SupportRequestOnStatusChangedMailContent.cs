namespace App.EventHandler.Impl.Support
{
    using App.Common;
    using App.Common.Helpers;
    using App.Common.Mail;
    using App.EventHandler.Support;

    internal class SupportRequestOnStatusChangedMailContent : EmailContent
    {
        public SupportRequestOnStatusChangedMailContent(SupportRequestOnStatusChanged ev) : base()
        {
            this.Subject = ResourceHelper.ToResourceKey(ev.Subject, ResourceType.Text);
            this.Body = ResourceHelper.ToResourceKey("support\\supportRequestOnStatusChanged.html", ResourceType.MailTemplate);
            this.To = ev.RequestorEmail;
        }
    }
}