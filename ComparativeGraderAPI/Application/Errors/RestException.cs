using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ComparativeGraderAPI.Application.Errors
{
    public class RestException : Exception
    {
        public RestException(HttpStatusCode code, object erros = null)
        {
            Code = code;
            Errors = erros;
        }

        public HttpStatusCode Code { get; set; }
        public object Errors { get; }
    }
}
