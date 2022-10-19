using LadyO.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LadyO.API.Controllers
{
    public class CommuneController : ApiController
    {
        [Route("api/Commune/getObject/{idCommune}")]
        [HttpGet]
        public object getObject(int idCommune)
        {
            try
            {
                return Models.Commune.getObject(idCommune);
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

        [Route("api/Commune/objAdd")]
        [HttpPost]
        public object objAdd([FromBody] Models.Commune obj)
        {
            APIGenericResponse response = new APIGenericResponse();
            try
            {
                object objReturn = new object();
                if (ModelState.IsValid)
                {
                    return Models.Commune.objAdd(obj);
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

        [Route("api/Commune/objUpdate")]
        [HttpPut]
        public object objUpdate([FromBody] Models.Commune obj)
        {
            APIGenericResponse response = new APIGenericResponse();
            try
            {
                if (ModelState.IsValid)
                {
                    return Models.Commune.objUpdate(obj);
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

        [Route("api/Commune/objDelete")]
        [HttpDelete]
        public object objDelete([FromBody] Models.Commune obj)
        {
            APIGenericResponse response = new APIGenericResponse();
            try
            {
                if (ModelState.IsValid)
                {
                    return Models.Commune.objDelete(obj);
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

        [Route("api/Commune/getList")]
        [HttpGet]
        public object getList()
        {
            try
            {
                return Models.Commune.getList();
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

        [Route("api/Commune/getListAdm/{idPerson}")]
        [HttpGet]
        public object getListAdm(int idPerson)
        {
            try
            {
                return Models.Commune.getListAdm(idPerson);
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