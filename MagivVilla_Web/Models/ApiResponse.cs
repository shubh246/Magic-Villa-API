﻿using System.Net;

namespace MagivVilla_Web.Models
{
    public class ApiResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public List<string> ErrorMessage { get; set; }
        public bool IsSuccess { get; set; } = true;
        public Object Result { get; set; }

    }
}
