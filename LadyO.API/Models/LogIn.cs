using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LadyO.API.Models
{
    public class LogIn
    {
        public string eMail { get; set; }

        public LogIn()
        {

        }
        public LogIn(string eMail)
        {
            this.eMail = eMail;
        }

        public static bool IsUserValid(string objMail)
        {
            try
            {
                List<LogIn> objListadoCorreos = new List<LogIn>();
                objListadoCorreos.Add(new LogIn("mcastillo@guiasyscoutschile.cl"));
                objListadoCorreos.Add(new LogIn("cugarte@guiasyscoutschile.cl"));
                objListadoCorreos.Add(new LogIn("rarenas@guiasyscoutschile.cl"));
                objListadoCorreos.Add(new LogIn("mmontero@guiasyscoutschile.cl"));
                if (objListadoCorreos.FirstOrDefault(x => x.eMail.ToLower() == objMail.ToLower()) != null)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static bool IsTokenValid(string token)
        {
            try
            {
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static object LogInUser(LogIn objLogIn)
        {
            APIGenericLogInResponse response = new APIGenericLogInResponse();
            try
            {
                if (Generic.Tools.ValidarEmail(objLogIn.eMail))
                {
                    if (objLogIn.eMail.ToLower().Contains("@guiasyscoutschile.cl"))
                    {
                        response.isValid = true;
                        response.token = Generic.Tools.TokenGen(30);
                        response.msg = string.Empty;
                    }
                    else
                    {
                        response.isValid = false;
                        response.token = string.Empty;
                        response.msg = Generic.Message.LOGIN_USUARIO_NO_VALIDO;
                    }
                }
                else
                {
                    response.isValid = false;
                    response.token = string.Empty;
                    response.msg = Generic.Message.EMAIL_INVALIDO;
                }
                return new { response };
            }
            catch (Exception ex)
            {
                response.isValid = false;
                response.token = string.Empty;
                response.msg = ex.Message;
                return new { response };
            }
        }
    }
}