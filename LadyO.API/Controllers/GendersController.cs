using LadyO.API.Models;
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
        [Route("api/Genders/getList/{token}")]
        [HttpGet]
        public object getList(string token)
        {
            try
            {
                object objReturn = new object();
                if (Models.LogIn.IsTokenValid(token))
                {
                    objReturn = Models.Genders.getList();
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
        [Route("api/Genders/getObject/{token}/{id}")]
        [HttpGet]
        public object getObject(string token, int id)
        {
            try
            {
                object objReturn = new object();
                if (Models.LogIn.IsTokenValid(token))
                {
                    objReturn = Models.Genders.getObject(id);
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

        [Route("api/Genders/objAdd/{token}")]
        [HttpPost]
        public object objAdd(string token, [FromBody] Models.Genders obj)
        {
            APIGenericResponse response = new APIGenericResponse();
            try
            {
                object objReturn = new object();
                if (Models.LogIn.IsTokenValid(token))
                {
                    if (ModelState.IsValid)
                    {
                        return Models.Genders.objAdd(obj);
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



        [Route("api/Genders/ObjUpdate/{token}")]
        [HttpPut]
        public object ObjUpdate(string token, [FromBody] Models.Genders objUpdate)
        {
            APIGenericResponse response = new APIGenericResponse();
            try
            {
                if (Models.LogIn.IsTokenValid(token))
                {
                    if (ModelState.IsValid)
                    {
                        return Models.Genders.objUpdate(objUpdate);
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