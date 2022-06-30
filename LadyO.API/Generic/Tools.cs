using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace LadyO.API.Generic
{
    public class Tools
    {
        public static string TokenGen(int largo)
        {
            Random numeroAzar = new Random();
            Random posLetra = new Random();
            string[] letras = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
            int valor = letras.Length;
            string stringPassword = string.Empty;
            for (int i = 0; i < largo; i++)
            {
                if ((numeroAzar.Next(0, 9) % 2 == 0))
                {
                    stringPassword += letras[posLetra.Next(0, letras.Length - 1)];
                }
                else
                {
                    stringPassword += numeroAzar.Next(0, 9).ToString();
                }
            }
            return stringPassword;
        }

        public static bool ValidarEmail(string email)
        {
            string expresion;
            expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, string.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}