using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LadyO.API.Controllers
{
    public class LogInController : ApiController
    {
        [Route("api/LogIn/Token/{token}")]
        [HttpGet]
        public object Token(string token)
        {
            try
            {
                return new { isValid = true };
            }
            catch (Exception ex)
            {
                return new
                {
                    success = true,
                    error = true,
                    msg = ex.Message
                };
            }
        }

        [Route("api/LogIn/LogInUser")]
        [HttpPost]
        public object LogInUser([FromBody] Models.LogIn obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return Models.LogIn.LogInUser(obj);
                }
                else
                {
                    return new
                    {
                        success = true,
                        error = true,
                        msg = Generic.Message.OBJETO_NO_CORRESPONDE
                    };
                }
            }
            catch (Exception ex)
            {
                return new
                {
                    success = true,
                    error = true,
                    msg = ex.Message
                };
            }
        }
    }
}
