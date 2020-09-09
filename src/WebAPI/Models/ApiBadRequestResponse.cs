using System.Collections.Generic;
using System.Net;

namespace WebAPI.Models
{
    public class ApiBadRequestResponse : ApiResponse
    {
        public IEnumerable<string> Errors { get; }

        public ApiBadRequestResponse(IEnumerable<string> errors) 
            : base((int)HttpStatusCode.BadRequest)
        {
            Errors = errors;
        }
    }
}
