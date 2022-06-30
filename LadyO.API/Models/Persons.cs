using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LadyO.API.Models
{
    public class Persons
    {
        #region Variables
        public int MyProperty { get; set; }
        #endregion

        public static string LogIn(string eMail)
        {
            try
            {
                if (Generic.Tools.ValidarEmail(eMail))
                {
                    return Generic.Tools.TokenGen(50);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}