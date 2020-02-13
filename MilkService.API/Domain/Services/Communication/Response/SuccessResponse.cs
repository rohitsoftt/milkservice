using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MilkService.API.Domain.Services.Communication.Response
{
    public class SuccessResponse: OResponse
    {
        public SuccessResponse(List<string> messages): base(true, messages)
        {
        }
        public SuccessResponse(string message): base(true, message)
        {
        }
    }
}
