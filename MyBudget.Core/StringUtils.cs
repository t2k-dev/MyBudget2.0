using System;
using System.Collections.Generic;
using System.Text;

namespace MyBudget.Core
{
    public static class StringUtils
    {
        public static string GeneratePassword()
        {
            string password = "";
            string[] arr = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "B", "C", "D", "F", "G", "H", "J", "K", "L", "M", "N", "P", "Q", "R", "S", "T", "V", "W", "X", "Z", "b", "c", "d", "f", "g", "h", "j", "k", "m", "n", "p", "q", "r", "s", "t", "v", "w", "x", "z", "A", "E", "U", "Y", "a", "e", "i", "o", "u", "y" };
            Random rnd = new Random();
            for (int i = 0; i < 6; i++)
            {
                password = password + arr[rnd.Next(0, 57)];
            }
            return password;
        }
    }
}
