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
    }
}
