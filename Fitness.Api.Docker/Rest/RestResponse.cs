using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Api.Docker.Rest
{
    public class RestResponse
    {
        public string HRef { get; set; }
        public List<string> Allowed { get; set; }
        public string ContentType { get; set; }
        public int StatusCode { get; set; }
        public int ExtendedCode { get; set; }
        public string Message { get; set; }
        public IRestResource Resource { get; set; }
    }
}
