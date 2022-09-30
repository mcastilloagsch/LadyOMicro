using LadyO.API.Models;
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
        [Route("api/Sexes/getList/{token}")]
        [HttpGet]
        public object getList(string token)
        {
            try
            {
                object objReturn = new object();
                if (Models.LogIn.IsTokenValid(token))
                {
                    objReturn = Models.Sexes.getList();
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
        [Route("api/Sexes/getObject/{token}/{id}")]
        [HttpGet]
        public object getObject(string token, int id)
        {
            APIGenericResponse response = new APIGenericResponse();
            try
            {
                object objReturn = new object();
                if (Models.LogIn.IsTokenValid(token))
                {
                    objReturn = Models.Sexes.getObject(id);
                }
                else
                {
                    objReturn = LogIn.TokenInvalid();
                }
                return objReturn;
            }
            catch (Exception ex)
            {
                response.isValid = false;
                response.msg = ex.Message;
                response.data = null;
                return response;
            }
        }
        [Route("api/Sexes/objAdd/{token}")]
        [HttpPost]
        public object objAdd(string token, [FromBody] Models.Sexes obj)
        {
            APIGenericResponse response = new APIGenericResponse();
            try
            {
                object objReturn = new object();
                if (Models.LogIn.IsTokenValid(token))
                {
                    if (ModelState.IsValid)
                    {
                        return Models.Sexes.objAdd(obj);
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


        [Route("api/Sexes/objUpdate/{token}")]
        [HttpPut]
        public object objUpdate(string token, [FromBody] Models.Sexes objUpdate)
        {
            APIGenericResponse response = new APIGenericResponse();
            try
            {
                if (Models.LogIn.IsTokenValid(token))
                {
                    if (ModelState.IsValid)
                    {
                        return Models.Sexes.objUpdate(objUpdate);
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