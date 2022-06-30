using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LadyO.API.Controllers
{
    public class LogInController : ApiController
    {
        [Route("api/LogIn/Token/{token}")]
        [HttpGet]
        public object Token(string token)
        {
            try
            {
                return new { isValid = true };
            }
            catch (Exception ex)
            {
                return new
                {
                    success = true,
                    error = true,
                    msg = ex.Message
                };
            }
        }

        [Route("api/LogIn/LogIn")]
        [HttpPost]
        public object LogIn([FromBody] Models.LogIn obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string token = Models.Persons.LogIn(obj.eMail);
                    if (Generic.Tools.ValidarEmail(obj.eMail))
                    {
                        if (token != null)
                        {
                            bool isValid = Models.LogIn.IsUserValid(obj.eMail);
                            if (isValid)
                            {
                                return new
                                {
                                    isValid = true,
                                    token = token,
                                    msg = "Usuario Valido"
                                }; 
                            }
                            else
                            {
                                return new
                                {
                                    isValid = false,
                                    token = string.Empty,
                                    msg = "Usuario No Valido"
                                };
                            }
                        }
                        else
                        {
                            return new
                            {
                                isValid = true,
                                token = string.Empty,
                                msg = "Usuario No Valido"
                            };
                        }
                    }
                    else
                    {
                        return new
                        {
                            isValid = false,
                            token = string.Empty,
                            msg = "Usuario No Valido"
                        };
                    }
                }
                else
                {
                    return new
                    {
                        success = true,
                        error = true,
                        msg = Generic.Message.OBJETO_NO_CORRESPONDE
                    };
                }
            }
            catch (Exception ex)
            {
                return new
                {
                    success = true,
                    error = true,
                    msg = ex.Message
                };
            }
        }
    }
}
