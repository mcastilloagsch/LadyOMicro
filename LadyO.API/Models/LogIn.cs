using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LadyO.API.Models
{
    public class LogIn
    {
        public int idUser { get; set; }
        public string eMail { get; set; }
        public string token { get; set; }

        public LogIn()
        {

        }

        public LogIn(int idUser, string eMail, string token)
        {
            this.idUser = idUser;
            this.eMail = eMail;
            this.token = token;
        }

        public static object LogInUser(LogIn objLogIn)
        {
            APIGenericResponse response = new APIGenericResponse();
            response.data = null;
            try
            {
                if (objLogIn.eMail.Length != 0)
                {
                    if (Generic.Tools.ValidarEmail(objLogIn.eMail))
                    {
                        if (objLogIn.eMail.ToLower().Contains("@guiasyscoutschile.cl"))
                        {
                            response.isValid = true;
                            response.msg = string.Empty;
                            LogIn obj = new LogIn();
                            obj.idUser = 1;
                            obj.eMail = objLogIn.eMail.ToLower();
                            obj.token = Generic.Tools.TokenGen(30);
                            response.data = obj;
                        }
                        else
                        {
                            response.isValid = false;
                            response.msg = Generic.Message.LOGIN_USUARIO_NO_VALIDO;
                        }
                    }
                    else
                    {
                        response.isValid = false;
                        response.msg = Generic.Message.EMAIL_INVALIDO;
                    }
                }
                else
                {
                    response.isValid = false;
                    response.msg = Generic.Message.EMAIL_VACIO;
                }
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static object TokenInvalid()
        {
            try
            {
                APIGenericResponse response = new APIGenericResponse();
                response.data = null;
                response.isValid = false;
                response.msg = Generic.Message.TOKEN_INVALIDO_EXPIRADO;
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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
    }
}