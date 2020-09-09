using System.Net;

namespace WebAPI.Models
{
    public class ApiOkResponse : ApiResponse
    {
        public object Data { get; }

        public ApiOkResponse(object result) 
            : base((int)HttpStatusCode.OK)
        {
            Data = result;
        }
    }
}
