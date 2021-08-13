using System.Net.Mail;
using configurator_shop.Interfaces;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MailKit.Net.Smtp;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace configurator_shop.Services
{
    public class GmailSmtpEmailSender : ISmtpEmailSender
    {
        private readonly IConfiguration _configuration;

        public GmailSmtpEmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public EmailResult TryToSendMail(MailboxAddress to, string subject, MimeEntity body)
        {
            MimeMessage message = new MimeMessage();
            
            var from = new MailboxAddress("noreply", _configuration["SmtpConfiguration:SmtpUser"]);
            
            message.From.Add(from);
            message.To.Add(to);
            message.Subject = subject;
            message.Body = body;

            SmtpClient client = new SmtpClient();

            try
            {
                client.Connect(_configuration["SmtpConfiguration:SmtpServer"], 465, true);
                client.Authenticate(_configuration["SmtpConfiguration:SmtpUser"], _configuration["SmtpConfiguration:SmtpPassword"]);
                client.Send(message);
                client.Disconnect(true);
                client.Dispose();
            }
            catch
            {
                return EmailResult.SendFail;
            }
            
            return EmailResult.SendSuccess;
        }
    }
}