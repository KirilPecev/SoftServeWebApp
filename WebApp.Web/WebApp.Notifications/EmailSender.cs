namespace WebApp.Notifications
{
    using Entities;
    using MailKit.Net.Smtp;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Options;
    using MimeKit;
    using System;
    using System.Threading.Tasks;

    public class EmailSender : IEmailSender
    {
        private readonly EmailSettings emailSettings;
        private readonly IHostingEnvironment env;

        public EmailSender(
            IOptions<EmailSettings> emailSettings,
            IHostingEnvironment env)
        {
            this.emailSettings = emailSettings.Value;
            this.env = env;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            try
            {
                var mimeMessage = new MimeMessage();

                mimeMessage.From.Add(new MailboxAddress(emailSettings.SenderName, emailSettings.Sender));

                mimeMessage.To.Add(new MailboxAddress(email));

                mimeMessage.Subject = subject;

                mimeMessage.Body = new TextPart("html")
                {
                    Text = message
                };

                using (var client = new SmtpClient())
                {
                    // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                    if (env.IsDevelopment())
                    {
                        // The third parameter is useSSL (true if the client should make an SSL-wrapped
                        // connection to the server; otherwise, false).
                        await client.ConnectAsync(emailSettings.MailServer, emailSettings.MailPort, true);
                    }
                    else
                    {
                        await client.ConnectAsync(emailSettings.MailServer);
                    }

                    // Note: only needed if the SMTP server requires authentication
                    await client.AuthenticateAsync(emailSettings.Sender, emailSettings.Password);

                    await client.SendAsync(mimeMessage);

                    await client.DisconnectAsync(true);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}