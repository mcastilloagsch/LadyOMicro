using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LadyO.API.Models
{
    public class APIGenericLogInResponse
    {
        public bool isValid { get; set; }
        public string token { get; set; }
        public string msg { get; set; }

        public APIGenericLogInResponse()
        {

        }

        public APIGenericLogInResponse(bool isValid, string token, string msg)
        {
            this.isValid = isValid;
            this.token = token;
            this.msg = msg;
        }
    }
}