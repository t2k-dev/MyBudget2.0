using System;
using System.Collections.Generic;
using System.Text;

namespace MyBudget.Core.Interfaces
{
    public interface IEmailSenderService
    {
        /// <summary>
        /// Send email after registration.
        /// </summary>
        /// <param name="userEmail"></param>
        public void SendRegistrationEmail(string userEmail);

        /// <summary>
        /// Send email with new password.
        /// </summary>
        /// <param name="userEmail"></param>
        /// <param name="password"></param>
        public void SendResetPasswordEmail(string userEmail, string password);
    }
}
