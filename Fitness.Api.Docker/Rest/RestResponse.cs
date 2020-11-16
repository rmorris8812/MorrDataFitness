// ***************************************************************
// Copyright 2020 MorrData LLC. All rights reserved.
// ***************************************************************
using System.Collections.Generic;

namespace Fitness.Api.Rest
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
