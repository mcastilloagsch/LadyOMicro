using LadyO.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LadyO.API.Controllers
{
    public class ProvinceController : ApiController
    {
        [Route("api/Province/getObject/{idProvince}")]
        [HttpGet]
        public object getObject(int idProvince)
        {
            try
            {
                return Models.Province.getObject(idProvince); ;
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

        [Route("api/Province/objAdd")]
        [HttpPost]
        public object objAdd([FromBody] Models.Province obj)
        {
            APIGenericResponse response = new APIGenericResponse();
            try
            {
                object objReturn = new object();
                if (ModelState.IsValid)
                {
                    return Models.Province.objAdd(obj);
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

        [Route("api/Province/objUpdate")]
        [HttpPut]
        public object objUpdate([FromBody] Models.Province obj)
        {
            APIGenericResponse response = new APIGenericResponse();
            try
            {
                if (ModelState.IsValid)
                {
                    return Models.Province.objUpdate(obj);
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

        [Route("api/Province/objDelete")]
        [HttpDelete]
        public object objDelete([FromBody] Models.Province obj)
        {
            APIGenericResponse response = new APIGenericResponse();
            try
            {
                if (ModelState.IsValid)
                {
                    return Models.Province.objDelete(obj);
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

        [Route("api/Province/getList")]
        [HttpGet]
        public object getList()
        {
            try
            {
                return Models.Province.getList(); ;
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

        [Route("api/Province/getListAdm/{idPerson}")]
        [HttpGet]
        public object getListAdm(int idPerson)
        {
            try
            {
                return Models.Province.getListAdm(idPerson); ;
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