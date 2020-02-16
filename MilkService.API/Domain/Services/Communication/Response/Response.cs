using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MilkService.API.Domain.Services.Communication.Response
{
    public abstract class OResponse
    {
        public bool Success { get; private set; }
        public List<string> Messages { get; private set; }
        public OResponse(bool result, List<string> messages)
        {
            this.Messages = messages ?? new List<string>();
            this.Success = result;

        }
        public OResponse(bool result, string message)
        {
            this.Messages = new List<string>();
            this.Success = result;

            if (!string.IsNullOrWhiteSpace(message))
            {
                this.Messages.Add(message);
            }
        }
    }
}
