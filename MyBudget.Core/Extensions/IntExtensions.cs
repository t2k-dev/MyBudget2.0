using System;
using System.Collections.Generic;
using System.Text;

namespace MyBudget.Core.Extensions
{
    public static class IntExtensions
    {
        public static void CheckMoreThenZero(this int i, string elementName)
        {
            if (i < 1)
            {
                throw new ArgumentException($"\"{elementName}\" must be more than 0.");
            }

        }
    }
}
