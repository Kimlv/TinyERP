namespace App.Common.Mail
{
    using System.Collections.Generic;

    public interface IEmailContent
    {
        string From { get; set; }
        string To { get; set; }
        string Subject { get; set; }
        string Body { get; set; }
        IList<string> Attachments { get; set; }
    }
}