using LadyO.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LadyO.API.Controllers
{
    public class CountryController : ApiController
    {
        [Route("api/Country/getObject/{idCountry}")]
        [HttpGet]
        public object getObject(int idCountry)
        {
            try
            {
                return Models.Country.getObject(idCountry); ;
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

        [Route("api/Country/objAdd")]
        [HttpPost]
        public object objAdd([FromBody] Models.Country obj)
        {
            APIGenericResponse response = new APIGenericResponse();
            try
            {
                object objReturn = new object();
                if (ModelState.IsValid)
                {
                    return Models.Country.objAdd(obj);
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

        [Route("api/Country/objUpdate")]
        [HttpPut]
        public object objUpdate([FromBody] Models.Country obj)
        {
            APIGenericResponse response = new APIGenericResponse();
            try
            {
                if (ModelState.IsValid)
                {
                    return Models.Country.objUpdate(obj);
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

        [Route("api/Country/objDelete")]
        [HttpDelete]
        public object objDelete([FromBody] Models.Country obj)
        {
            APIGenericResponse response = new APIGenericResponse();
            try
            {
                if (ModelState.IsValid)
                {
                    return Models.Country.objDelete(obj);
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

        [Route("api/Country/getList")]
        [HttpGet]
        public object getList()
        {
            try
            {
                return Models.Country.getList(); ;
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

        [Route("api/Country/getListAdm/{idPerson}")]
        [HttpGet]
        public object getListAdm(int idPerson)
        {
            try
            {
                return Models.Country.getListAdm(idPerson); ;
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