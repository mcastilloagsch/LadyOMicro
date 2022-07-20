using LadyO.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LadyO.API.Controllers
{
    public class UnitAttributesController : ApiController
    {

        [Route("api/UnitAttributes/getList/{token}")]
        [HttpGet]
        public object getList(string token)
        {
            APIGenericResponse response = new APIGenericResponse();
            try
            {
                if (Models.LogIn.IsTokenValid(token))
                {
                    return Models.UnitAttributes.getList();
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

        [Route("api/UnitAttributes/getObject/{token}/{structure_id}/{branch_id}")]
        [HttpGet]
        public object getObject(string token, int structure_id, int branch_id)
        {
            APIGenericResponse response = new APIGenericResponse();
            try
            {
                if (Models.LogIn.IsTokenValid(token))
                {
                    return Models.UnitAttributes.getObject(structure_id, branch_id);
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

        [Route("api/UnitAttributes/ObjInsert/{token}")]
        [HttpPost]
        public object ObjInsert(string token, [FromBody] Models.UnitAttributes objInsert)
        {
            APIGenericResponse response = new APIGenericResponse();
            try
            {
                if (Models.LogIn.IsTokenValid(token))
                {
                    if (ModelState.IsValid)
                    {
                        return new { success = Models.UnitAttributes.ObjInsert(objInsert) };
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
