namespace MyBudget.Core.Enums
{
    /// <summary>
    /// Common transaction categories.
    /// </summary>
    public enum SystemCategories
    {
        /// <summary>
        /// "Без Категории" for income transactions. ID = 1
        /// </summary>
        IncomeNoCategory = 1,

        /// <summary>
        /// "Без Категории" for spending transactions. ID = 2
        /// </summary>
        SpendingNoCategory = 2,

        /// <summary>
        /// Take money from somebody
        /// </summary>
        TakeDebt = 3,

        /// <summary>
        /// Give money to somebody
        /// </summary>
        GiveCredit = 4,

        /// <summary>
        /// Rest from the previous month
        /// </summary>
        Rest = 5,

        /// <summary>
        /// Give borrowed money
        /// </summary>
        PayCredit = 6,

        /// <summary>
        ///  Return given money
        /// </summary>
        RecieveDebt = 7,

        /// <summary>
        /// Put money for a Goal
        /// </summary>
        PayGoal = 8
    }
}
