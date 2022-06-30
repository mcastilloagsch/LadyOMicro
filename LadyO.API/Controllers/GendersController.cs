using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LadyO.API.Controllers
{
    public class GendersController : ApiController
    {
        [Route("api/Genders/ObjInsert/{token}")]
        [HttpPost]
        public object ObjInsert(string token, [FromBody] Models.Genders objInsert)
        {
            try
            {
                if (Models.LogIn.IsTokenValid(token))
                {
                    if (ModelState.IsValid)
                    {
                        return new { success = Models.Genders.ObjInsert(objInsert) };
                    }
                    else
                    {
                        return new
                        {
                            success = false,
                            msg = Generic.Message.OBJETO_NO_CORRESPONDE
                        };
                    }
                }
                else
                {
                    return new
                    {
                        success = false,
                        msg = Generic.Message.TOKEN_INVALIDO_EXPIRADO
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

        [Route("api/Genders/ObjUpdate/{token}")]
        [HttpPut]
        public object ObjUpdate(string token, [FromBody] Models.Genders objUpdate)
        {
            try
            {
                if (Models.LogIn.IsTokenValid(token))
                {
                    if (ModelState.IsValid)
                    {
                        return new
                        {
                            success = Models.Genders.ObjUpdate(objUpdate)
                        };
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
                else
                {
                    return new
                    {
                        success = false,
                        msg = Generic.Message.TOKEN_INVALIDO_EXPIRADO
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

        [Route("api/Genders/ObjList/{token}")]
        [HttpGet]
        public object ObjList(string token)
        {
            try
            {
                if (Models.LogIn.IsTokenValid(token))
                {
                    return new
                    {
                        success = true,
                        obj = Models.Genders.ObjList()
                    };
                }
                else
                {
                    return new
                    {
                        success = false,
                        msg = Generic.Message.TOKEN_INVALIDO_EXPIRADO
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
