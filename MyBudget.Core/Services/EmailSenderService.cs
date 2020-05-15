using MyBudget.Core.Configs;
using MyBudget.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace MyBudget.Core.Services
{
    public class EmailSenderService : IEmailSenderService
    {
        #region ctor & fields

        private readonly EmailConfiguration _emailConfig;

        public EmailSenderService(EmailConfiguration emailConfig)
        {
            _emailConfig = emailConfig;
        }

        #endregion

        public void SendRegistrationEmail(string userEmail)
        {
            if (!_emailConfig.IsEnabled)
            {
                return;
            }

            using (var message = new MailMessage())
            {
                message.From = new MailAddress(_emailConfig.From, "MyBudgetSender");
                message.To.Add(new MailAddress(userEmail));
                message.Subject = "Welcome to MyBudget team!";
                message.Body = "Dear customer, welcome to our super system!";
                message.IsBodyHtml = true;

                Send(message);
            }
        }

        private void Send(MailMessage mailMessage)
        {
            using (var client = new SmtpClient(_emailConfig.SmtpServer))
            {
                try
                {
                    client.Port = _emailConfig.Port;
                    client.Credentials = new NetworkCredential(_emailConfig.UserName, _emailConfig.Password);
                    client.EnableSsl = true;
                    client.Send(mailMessage);
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    client.Dispose();
                }
            }
        }
    }
}
