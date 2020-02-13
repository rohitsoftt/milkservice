using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MilkService.API.Domain.DBExceptions
{
    public class DublicateEntryException: Exception
    {
        public DublicateEntryException(string Message) : base(Message)
        {

        }
    }
}
