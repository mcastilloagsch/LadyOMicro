using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LadyO.API.Models
{
    public class APIGenericResponse
    {
        public bool isValid { get; set; }
        public string msg { get; set; }
        public object data { get; set; }

        public APIGenericResponse()
        {

        }

        public APIGenericResponse(bool isValid, string msg, object data)
        {
            this.isValid = isValid;
            this.msg = msg;
            this.data = data;
        }
    }
}