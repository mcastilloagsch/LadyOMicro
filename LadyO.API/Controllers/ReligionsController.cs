using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LadyO.API.Controllers
{
    public class ReligionsController : ApiController
    {
        [Route("api/Religions/ObjInsert/{token}")]
        [HttpPost]
        public object ObjInsert(string token, [FromBody] Models.Religions objInsert)
        {
            try
            {
                if (Models.LogIn.IsTokenValid(token))
                {
                    if (ModelState.IsValid)
                    {
                        return new { success = Models.Religions.ObjInsert(objInsert) };
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

        [Route("api/Religions/ObjUpdate/{token}")]
        [HttpPut]
        public object ObjUpdate(string token, [FromBody] Models.Religions objUpdate)
        {
            try
            {
                if (Models.LogIn.IsTokenValid(token))
                {
                    if (ModelState.IsValid)
                    {
                        return new
                        {
                            success = Models.Religions.ObjUpdate(objUpdate)
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

        [Route("api/Religions/ObjList/{token}")]
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
                        obj = Models.Religions.ObjList()
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