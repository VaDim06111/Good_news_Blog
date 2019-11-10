using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace Good_news_Blog.EmailService
{
    public class SmtpEmailService : IEmailSender
    {
        public IConfiguration Configuration { get; }
        public SmtpEmailService(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var HOST = Configuration.GetSection("EmailSender:HOST").Value;
            var PORT = Configuration.GetSection("EmailSender:PORT").Value;
            var OWNER_EMAIL = Configuration.GetSection("EmailSender:OWNER_EMAIL").Value;
            var OWNER_PASSWORD = Configuration.GetSection("EmailSender:OWNER_PASSWORD").Value;

            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Администрация Good news Blog", HOST));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = htmlMessage
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(HOST, Int32.Parse(PORT), false);
                await client.AuthenticateAsync(OWNER_EMAIL, OWNER_PASSWORD);
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}
