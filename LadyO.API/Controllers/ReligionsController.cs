using LadyO.API.Models;
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

        [Route("api/Religions/getList/{token}")]
        [HttpGet]
        public object getList(string token)
        {
            try
            {
                object objReturn = new object();
                if (Models.LogIn.IsTokenValid(token))
                {
                    objReturn = Models.Religions.getList();
                }
                else
                {
                    objReturn = LogIn.TokenInvalid();
                }
                return objReturn;
            }
            catch (Exception ex)
            {
                APIGenericResponse response = new APIGenericResponse();
                response.isValid = false;
                response.msg = ex.Message;
                response.data = null;
                return response;
            }
        }

        [Route("api/Religions/getObject/{token}/{id}")]
        [HttpGet]
        public object getObject(string token, int id)
        {
            try
            {
                object objReturn = new object();
                if (Models.LogIn.IsTokenValid(token))
                {
                    objReturn = Models.Religions.getObject(id);
                }
                else
                {
                    objReturn = LogIn.TokenInvalid();
                }
                return objReturn;
            }
            catch (Exception ex)
            {
                APIGenericResponse response = new APIGenericResponse();
                response.isValid = false;
                response.msg = ex.Message;
                response.data = null;
                return response;
            }
        }

        [Route("api/Religions/objAdd/{token}")]
        [HttpPost]
        public object objAdd(string token, [FromBody] Models.Religions obj)
        {
            APIGenericResponse response = new APIGenericResponse();
            try
            {
                object objReturn = new object();
                if (Models.LogIn.IsTokenValid(token))
                {
                    if (ModelState.IsValid)
                    {
                        return Models.Religions.objAdd(obj);
                    }
                    else
                    {
                        return LogIn.TokenInvalid();
                    }
                }
                else
                {
                    response.isValid = false;
                    response.msg = Generic.Message.TOKEN_INVALIDO_EXPIRADO;
                    response.data = null;
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.isValid = false;
                response.msg = ex.Message;
                response.data = null;
                return response;
            }
        }

        [Route("api/Religions/ObjUpdate/{token}")]
        [HttpPut]
        public object ObjUpdate(string token, [FromBody] Models.Religions objUpdate)
        {
            APIGenericResponse response = new APIGenericResponse();
            try
            {
                if (Models.LogIn.IsTokenValid(token))
                {
                    if (ModelState.IsValid)
                    {
                        return Models.Religions.objUpdate(objUpdate);
                    }
                    else
                    {
                        response.isValid = false;
                        response.msg = Generic.Message.OBJETO_NO_CORRESPONDE;
                        response.data = null;
                        return response;
                    }
                }
                else
                {
                    return LogIn.TokenInvalid();
                }
            }
            catch (Exception ex)
            {
                response.isValid = false;
                response.msg = ex.Message;
                response.data = null;
                return response;
            }
        }

    }
}
