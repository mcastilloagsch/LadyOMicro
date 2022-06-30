using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LadyO.API.Controllers
{
    public class SexesController : ApiController
    {
        [Route("api/Sexes/ObjInsert/{token}")]
        [HttpPost]
        public object ObjInsert(string token, [FromBody] Models.Sexes objInsert)
        {
            try
            {
                if (Models.LogIn.IsTokenValid(token))
                {
                    if (ModelState.IsValid)
                    {
                        return new { success = Models.Sexes.ObjInsert(objInsert) };
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

        [Route("api/Sexes/ObjUpdate/{token}")]
        [HttpPut]
        public object ObjUpdate(string token, [FromBody] Models.Sexes objUpdate)
        {
            try
            {
                if (Models.LogIn.IsTokenValid(token))
                {
                    if (ModelState.IsValid)
                    {
                        return new
                        {
                            success = Models.Sexes.ObjUpdate(objUpdate)
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

        [Route("api/Sexes/ObjList/{token}")]
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
                        obj = Models.Sexes.ObjList()
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