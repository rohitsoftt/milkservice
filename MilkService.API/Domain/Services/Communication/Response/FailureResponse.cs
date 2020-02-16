using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MilkService.API.Domain.Services.Communication.Response
{
    public class FailureResponse: OResponse
    {
        public FailureResponse(List<string> messages): base(false, messages)
        {
        }
        public FailureResponse(string message): base(false,message)
        {
        }
    }
}
