using LadyO.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LadyO.API.Controllers
{
    public class CountriesController : ApiController
    {

        [Route("api/Countries/getList/{token}")]
        [HttpGet]
        public object getList(string token)
        {
            APIGenericResponse response = new APIGenericResponse();
            try
            {
                if (Models.LogIn.IsTokenValid(token))
                {
                    return Models.Countries.getList();
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

        [Route("api/Countries/getObject/{token}/{id}")]
        [HttpGet]
        public object getObject(string token, int id)
        {
            APIGenericResponse response = new APIGenericResponse();
            try
            {
                if (Models.LogIn.IsTokenValid(token))
                {
                    return Models.Countries.getObject(id);
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

        [Route("api/Countries/ObjInsert/{token}")]
        [HttpPost]
        public object ObjInsert(string token, [FromBody] Models.Countries objInsert)
        {
            APIGenericResponse response = new APIGenericResponse();
            try
            {
                if (Models.LogIn.IsTokenValid(token))
                {
                    if (ModelState.IsValid)
                    {
                        return new { success = Models.Countries.ObjInsert(objInsert) };
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

        [Route("api/Countries/ObjUpdate/{token}/{id}")]
        [HttpPost]
        public object ObjUpdate(string token,int id, [FromBody] Models.Countries objUpdate)
        {
            APIGenericResponse response = new APIGenericResponse();
            try
            {
                if (Models.LogIn.IsTokenValid(token))
                {
                    if (ModelState.IsValid)
                    {
                        return new
                        {
                            success = Models.Countries.ObjUpdate(objUpdate)
                        };
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

    }
}
