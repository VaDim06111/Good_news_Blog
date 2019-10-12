using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using MailKit.Net.Smtp;
using MimeKit;

namespace Good_news_Blog.EmailService
{
    public class SmtpEmailService : IEmailSender
    {
        private const string HOST = "smtp.gmail.com";
        private const int PORT = 587;
        private const string OWNER_EMAIL = "goodnewsbloga@gmail.com";
        private const string OWNER_PASSWORD = "qwe123ZXC.";

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
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
                await client.ConnectAsync(HOST, PORT, false);
                await client.AuthenticateAsync(OWNER_EMAIL, OWNER_PASSWORD);
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}
