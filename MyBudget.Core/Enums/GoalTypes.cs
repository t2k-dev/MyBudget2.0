namespace MyBudget.Core.Enums
{
    /// <summary>
    /// Goal types: 1 - Goal, 2 - Debt, 3 - Loan.
    /// </summary>
    public enum GoalTypes : byte
    {
        /// <summary>
        /// Type = 1
        /// </summary>
        Goal = 1,

        /// <summary>
        /// Type = 2
        /// </summary>
        Debt,

        /// <summary>
        /// Type = 3
        /// </summary>
        Loan
    }
}
