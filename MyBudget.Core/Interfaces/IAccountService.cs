using MyBudget.Core.Models;
using MyBudget.Core.Models.Account;
using System.Threading.Tasks;

namespace MyBudget.Core.Interfaces
{
    public interface IAccountService
    {
        /// <summary>
        /// Save initial properties for the user.
        /// </summary>
        public void SaveInitialCustomization(InitialCustomizationModel model);

        /// <summary>
        /// Gets the symbol of users default currency.
        /// </summary>
        /// <param name="userID">User ID (Guid as string)</param>
        /// <returns>Symbol like '₸', '$', etc. </returns>
        public string GetUserDefaultCurrencySymbol(string userID);

        /// <summary>
        /// Get user ID by user name.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns>User ID (Guid as string)</returns>
        public string GetUserIDByName(string userName);

        /// <summary>
        /// Gets the users default currency.
        /// </summary>
        /// <param name="userID">User ID (Guid as string)</param>
        /// <returns>Currency</returns>
        public CurrencyModel GetUserDefaultCurrency(string userID);

        /// <summary>
        /// Get users configuration.
        /// </summary>
        /// <param name="userID">User ID (Guid as string)</param>
        /// <returns>User Configs</returns>
        public UserConfigs GetUserConfigs(string userID);

        /// <summary>
        /// Save users configuration.
        /// </summary>
        /// <param name="userConfigs">Configs model</param>
        /// <param name="userID">User ID (Guid as string)</param>
        public void SaveConfig(UserConfigs userConfigs, string userID);

        /// <summary>
        /// Set UpdateDate for user with current date and time.
        /// </summary>
        /// <param name="userID">User ID (Guid as string)</param>
        public void RefreshUpdateDate(string userID);

        /// <summary>
        /// Register new user.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Task<string> RegisterAsync(UserModel user, string password);

        /// <summary>
        /// Change password for user.
        /// </summary>
        /// <param name="userID">User ID (Guid as string)</param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        public void ChangePasswordAsync(string userID, string oldPassword, string newPassword);

        /// <summary>
        /// Resets password and sends an email.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="email"></param>
        public Task ForgotPassword(string userName, string email);
    }
}
