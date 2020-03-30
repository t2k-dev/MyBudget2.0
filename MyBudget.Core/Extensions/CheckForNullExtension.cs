using System;

namespace MyBudget.Core.Extensions
{
    public static class CheckForNullExtension
    {
        public static void CheckForNull<T>(this T targetObject, string parameterName) where T : class
        {
            if (targetObject == null)
            {
                throw new ArgumentNullException(parameterName, $"Argument \"{parameterName}\" can not be null");
            }
        }

        public static void CheckForNull<T>(this T? targetObject, string parameterName) where T : struct
        {
            if (targetObject == null)
            {
                throw new ArgumentNullException(parameterName, $"Argument \"{parameterName}\" can not be null");
            }
        }

    }
}
