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
            APIGenericResponse response = new APIGenericResponse();
            try
            {
                if (Models.LogIn.IsTokenValid(token))
                {
                    return Models.Genders.getList();
                }
                else
                {
                    response.isValid = false;
                    response.msg = Generic.Message.TOKEN_INVALIDO_EXPIRADO;
                    response.data = null;
                    return new { response };
                }
            }
            catch (Exception ex)
            {
                response.isValid = false;
                response.msg = ex.Message;
                response.data = null;
                return new { response };
            }
        }
    }
}