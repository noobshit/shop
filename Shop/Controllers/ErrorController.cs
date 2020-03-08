using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shop.ViewModels;

namespace Shop.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> _logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger;
        }

        [Route("Error/{StatusCode}")]
        public IActionResult StatusCodeError(int statusCode)
        {
            var model = new ErrorViewModel 
            { 
                ErrorCode = statusCode 
            };
            return View("StatusCodePage", model);
        }

        [Route("Error")]
        public IActionResult Exception()
        {
            var exceptionHandlerFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            _logger.LogError($"The path {exceptionHandlerFeature.Path} threw and exception {exceptionHandlerFeature.Error}");
            return View("ExceptionPage");
        }
    }
}
