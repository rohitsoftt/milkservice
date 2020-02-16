using Microsoft.AspNetCore.Mvc;
using MilkService.API.Domain.Services.Communication.Response;
using MilkService.API.Extensions;
using MilkService.API.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MilkService.API.Controllers.Config
{
    public static class InvalidModelStateResponseFactory
    {
        public static IActionResult ProduceErrorResponse(ActionContext context)
        {
            var errors = context.ModelState.GetErrorMessages();
            var response = new FailureResponse(messages: errors);

            return new BadRequestObjectResult(response);
        }
    }
}
