using Application.DTOs.Commom;
using DesafioIngaCodeApi.ViewModel;
using Domain.Tools;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace DesafioIngaCodeApi.Middlewares
{
    public class ErrorMiddleware(RequestDelegate next, ILogger<ErrorMiddleware> logger)
    {
        private readonly RequestDelegate next = next;
        private readonly ILogger<ErrorMiddleware> _logger = logger;

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);

                await UnAuthorized(context);

            }
            catch (Exception ex)
            {

                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            _logger.LogError(ex, $"Error occurred with TraceId: {context.TraceIdentifier}");

            var problemDetails = new ProblemDetails
            {
                Instance = context.Request.Path,
                Type = "https://learn.microsoft.com/pt-br/troubleshoot/developer/webapps/iis/www-administration-management/http-status-code",
                Extensions =
                        {
                            { "traceId", context.TraceIdentifier },
                            { "Logref", Guid.NewGuid().ToString() },
                            { "Message", "Standard message that is not error specific"}
                        }
            };

            int statusCode;
            ErrorResponseVm errorResponseVm;

            switch (ex)
            {         
                case ArgumentException _:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    errorResponseVm = new ErrorResponseVm("ErroID400", "Invalid request.");
                    problemDetails.Extensions["Error specific message"] = "Error 400....explaining..";
                    problemDetails.Extensions["Learn more about the error"] = "https://learn.microsoft.com/pt-br/iis/troubleshoot/diagnosing-http-errors/troubleshooting-http-400-errors-in-iis";
                    break;

                //HTTP 401
                case UnauthorizedAccessException _:
                    statusCode = (int)HttpStatusCode.Unauthorized;
                    errorResponseVm = new ErrorResponseVm("ErroID401", "Not authorized.");
                    problemDetails.Extensions["Error specific message"] = ex.StackTrace;
                    problemDetails.Extensions["Learn more about the error"] = ex.Message;
                    break;

           
                //HTTP 500
                default:
                    statusCode = (int)HttpStatusCode.InternalServerError;
                    errorResponseVm = new ErrorResponseVm("ErroID500", "An internal server error has occurred.");
                    break;
            }

            problemDetails.Status = statusCode;
            problemDetails.Title = Tool.GetErrorTitle(statusCode);
            problemDetails.Detail = errorResponseVm.Errors.FirstOrDefault()?.Message;

            await ChangeContext(context, problemDetails, statusCode);
        }

        private static async Task UnAuthorized(HttpContext context)
        {
            if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
                await ChangeContext(context,
                    new ResultModel<dynamic>(HttpStatusCode.Unauthorized,
                    Tool.GetErrorTitle((int)HttpStatusCode.Unauthorized))
                    , (int)HttpStatusCode.Unauthorized);

            else if (context.Response.StatusCode == (int)HttpStatusCode.Forbidden)
                await ChangeContext(context,
                    new ResultModel<dynamic>(HttpStatusCode.Forbidden,
                    Tool.GetErrorTitle((int)HttpStatusCode.Forbidden))
                    , (int)HttpStatusCode.Forbidden);
        }

        public static async Task ChangeContext<T>(HttpContext context, T problemDetails, int status)
        {
            context.Response.StatusCode = status;
            var result = JsonConvert.SerializeObject(problemDetails);
            context.Response.ContentType = "application/problem+json";
            await context.Response.WriteAsync(result);
        }
    }
}