using System.Net;
using DotNetRuServerHipstaMVP.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace DotNetRuServerHipstaMVP.Api.Application.ExceptionFilter
{
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<ExceptionFilter> _logger;

        public ExceptionFilter(ILogger<ExceptionFilter> logger)
        {
            _logger = logger;
        }


        public void OnException(ExceptionContext context)
        {
            switch (context.Exception)
            {
                case ValidationException ex:
                    context.Result =
                        new NotAcceptableObjectResult(new ErrorResponse {Errors = ex.Exceptions});
                    return;

            }

            _logger.LogError(
                "exception - {0}",
                context.Exception.ToString()
            );

            context.Result =
                new InternalServerErrorObjectResult(new ErrorResponse { Errors = new[] { context.Exception.ToString() } });
           
            context.ExceptionHandled = true;
        }
    }

    public class ErrorResponse
    {
        public string[] Errors { get; set; }
    }
    
    public class InternalServerErrorObjectResult : ObjectResult
    {
        public InternalServerErrorObjectResult(object value)
            : base(value)
        {
            StatusCode = (int) HttpStatusCode.InternalServerError;
        }
    }
    
    public class NotAcceptableObjectResult : ObjectResult
    {
        public NotAcceptableObjectResult(object value)
            : base(value)
        {
            StatusCode = (int) HttpStatusCode.NotAcceptable;
        }
    }



}