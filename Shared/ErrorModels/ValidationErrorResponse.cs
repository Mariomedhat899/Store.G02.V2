using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ErrorModels
{
    public class ValidationErrorResponse
    {
        public int StatusCode { get; set; } = StatusCodes.Status400BadRequest;
        public string ErrorMessage { get; set; } = "Validation Errors !!";

        public IEnumerable<ValdationError> Errors { get; set; }
    }

    public class ValdationError
    {
        public string FieldName { get; set; } 
        public IEnumerable<string> Errors { get; set; } 

    }
}
