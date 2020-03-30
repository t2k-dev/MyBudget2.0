using MyBudget.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBudget.Core.Interfaces
{
    public interface IAccountService
    {
        /// <summary>
        /// Save initial properties for the user.
        /// </summary>
        public void SaveInitialCustomization(InitialCustomizationModel model);
    }
}
