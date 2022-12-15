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

        public static string Capital(string _texto)
        {
            if (_texto.Length < 1)
                return string.Empty;
            _texto = _texto.Trim().ToLower();
            char[] array = _texto.ToCharArray();
            if (array.Length >= 1)
            {
                if (char.IsLower(array[0]))
                {
                    array[0] = char.ToUpper(array[0]);
                }
            }
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i - 1] == ' ')
                {
                    if (char.IsLower(array[i]))
                    {
                        array[i] = char.ToUpper(array[i]);
                    }
                }
            }
            return new string(array);
        }

        public static bool ValidarDNI(int idDniType, string dni, string dniVerif)
        {
            try
            {
                switch (idDniType)
                {
                    case 1:
                        return ValidaRut(dni, dniVerif);
                    default:
                        return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Herramienta Validar RUT
        private static bool ValidaRut(string rut)
        {
            rut = rut.Replace(".", "").ToUpper();
            System.Text.RegularExpressions.Regex expresion = new System.Text.RegularExpressions.Regex("^([0-9]+-[0-9K])$");
            string dv = rut.Substring(rut.Length - 1, 1);
            if (!expresion.IsMatch(rut))
            {
                return false;
            }
            char[] charCorte = { '-' };
            string[] rutTemp = rut.Split(charCorte);
            if (dv != Digito(int.Parse(rutTemp[0])))
            {
                return false;
            }
            return true;
        }

        private static bool ValidaRut(string rut, string dv)
        {
            return ValidaRut(rut + "-" + dv);
        }

        private static string Digito(int rut)
        {
            int suma = 0;
            int multiplicador = 1;
            while (rut != 0)
            {
                multiplicador++;
                if (multiplicador == 8)
                    multiplicador = 2;
                suma += (rut % 10) * multiplicador;
                rut = rut / 10;
            }
            suma = 11 - (suma % 11);
            if (suma == 11)
            {
                return "0";
            }
            else if (suma == 10)
            {
                return "K";
            }
            else
            {
                return suma.ToString();
            }
        }
        #endregion
    }
}