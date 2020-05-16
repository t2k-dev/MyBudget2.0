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

        public void SendResetPasswordEmail(string userEmail, string password)
        {
            if (!_emailConfig.IsEnabled)
            {
                return;
            }

            var message = new MailMessage()
            {
                From = new MailAddress(_emailConfig.From, "MyBudgetSender"),
                Subject = "MyBudget password is reset.",
                Body = $"Dear customer, Your new password:{password}",
                IsBodyHtml = true
            };
            message.To.Add(new MailAddress(userEmail));

            Send(message);
        }

        #region Helpers

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
                    mailMessage.Dispose();
                    client.Dispose();
                }
            }
        }

        #endregion
    }
}
