using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using MimeKit;

namespace configurator_shop.Interfaces
{
    public enum EmailResult
    {
        SendSuccess,
        SendFail
    }
    public interface ISmtpEmailSender
    {
        public EmailResult TryToSendMail(MailboxAddress to, string subject, MimeEntity body);
    }
}