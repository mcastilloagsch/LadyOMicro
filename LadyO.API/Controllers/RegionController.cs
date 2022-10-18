using LadyO.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LadyO.API.Controllers
{
    public class RegionController : ApiController
    {
        [Route("api/Region/getObject/{idRegion}")]
        [HttpGet]
        public object getObject(int idRegion)
        {
            try
            {
                return Models.Region.getObject(idRegion); ;
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

        [Route("api/Region/objAdd")]
        [HttpPost]
        public object objAdd([FromBody] Models.Region obj)
        {
            APIGenericResponse response = new APIGenericResponse();
            try
            {
                object objReturn = new object();
                if (ModelState.IsValid)
                {
                    return Models.Region.objAdd(obj);
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

        [Route("api/Region/objUpdate")]
        [HttpPut]
        public object objUpdate([FromBody] Models.Region obj)
        {
            APIGenericResponse response = new APIGenericResponse();
            try
            {
                if (ModelState.IsValid)
                {
                    return Models.Region.objUpdate(obj);
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

        [Route("api/Region/objDelete")]
        [HttpDelete]
        public object objDelete([FromBody] Models.Region obj)
        {
            APIGenericResponse response = new APIGenericResponse();
            try
            {
                if (ModelState.IsValid)
                {
                    return Models.Region.objDelete(obj);
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

        [Route("api/Region/getList")]
        [HttpGet]
        public object getList()
        {
            try
            {
                return Models.Region.getList(); ;
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

        [Route("api/Region/getListAdm")]
        [HttpGet]
        public object getListAdm(int idPerson)
        {
            try
            {
                return Models.Region.getListAdm(idPerson); ;
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