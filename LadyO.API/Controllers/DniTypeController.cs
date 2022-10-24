using LadyO.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LadyO.API.Controllers
{
    public class DniTypeController : ApiController
    {
        [Route("api/DniType/getObject/{idDniType}")]
        [HttpGet]
        public object getObject(int idDniType)
        {
            try
            {
                return Models.DniType.getObject(idDniType);
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

        [Route("api/DniType/objAdd")]
        [HttpPost]
        public object objAdd([FromBody] Models.DniType obj)
        {
            APIGenericResponse response = new APIGenericResponse();
            try
            {
                object objReturn = new object();
                if (ModelState.IsValid)
                {
                    return Models.DniType.objAdd(obj);
                }
                else
                {
                    response.isValid = false;
                    response.msg = Generic.Message.OBJETO_NO_CORRESPONDE;
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

        [Route("api/DniType/objUpdate")]
        [HttpPut]
        public object objUpdate([FromBody] Models.DniType obj)
        {
            APIGenericResponse response = new APIGenericResponse();
            try
            {
                if (ModelState.IsValid)
                {
                    return Models.DniType.objUpdate(obj);
                }
                else
                {
                    response.isValid = false;
                    response.msg = Generic.Message.OBJETO_NO_CORRESPONDE;
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

        [Route("api/DniType/objDelete")]
        [HttpDelete]
        public object objDelete([FromBody] Models.DniType obj)
        {
            APIGenericResponse response = new APIGenericResponse();
            try
            {
                if (ModelState.IsValid)
                {
                    return Models.DniType.objDelete(obj);
                }
                else
                {
                    response.isValid = false;
                    response.msg = Generic.Message.OBJETO_NO_CORRESPONDE;
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

        [Route("api/DniType/getList")]
        [HttpGet]
        public object getList()
        {
            try
            {
                return Models.DniType.getList(); ;
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

        [Route("api/DniType/getListAdm/{idPerson}")]
        [HttpGet]
        public object getListAdm(int idPerson)
        {
            try
            {
                return Models.DniType.getListAdm(idPerson);
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
    }
}