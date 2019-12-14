using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using SmartRecruiting.Application.Exceptions;

namespace SmartRecruiting.API.Infrastructure {
    public class ExceptionHandlingMiddleware {
        private readonly RequestDelegate _next;
        public ExceptionHandlingMiddleware(RequestDelegate next) {
            _next = next;
        }

        public async Task Invoke(HttpContext context) {
            try {
                await _next(context);
            } catch (Exception ex) {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex) {
            // default http statuscode.
            var code = HttpStatusCode.InternalServerError;
            var result = string.Empty;

            // handle custom exceptions.
            switch (ex) {
                case AuthException authException:
                    {
                        code = HttpStatusCode.Unauthorized;
                        result = authException.Message;
                    }
                    break;
                case DuplicateEntityException duplicateEntityException:
                    {
                        code = HttpStatusCode.BadRequest;
                        result = duplicateEntityException.Message;
                    }
                    break;
                default:
                    {
                        code = HttpStatusCode.InternalServerError;
                        result = string.Empty;
                    }
                    break;
            }
            context.Response.ContentType = "applicaiton/json";
            context.Response.StatusCode = (int) code;

            if (result == string.Empty) {
                result = JsonConvert.SerializeObject(new { error = ex.Message });
            }
            return context.Response.WriteAsync(result);
        }
    }

    public static class ExceptionHandlingExtension {
        public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder builder) {
            return builder.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}