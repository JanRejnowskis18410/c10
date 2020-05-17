using Cw10.Exceptions;
using Cw10.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cw10.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exc)
            {
                await HandleExceptionAsync(context, exc);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exc)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            if (exc is DBException)
            {
                return context.Response.WriteAsync(new ErrorDetails
                {
                    StatusCode = (int)StatusCodes.Status400BadRequest,
                    Message = "Problem w bazie danych: " + exc.Message
                }.ToString());
            }

            if (exc is StudiesDoesNotExistException)
            {
                return context.Response.WriteAsync(new ErrorDetails
                {
                    StatusCode = (int)StatusCodes.Status400BadRequest,
                    Message = exc.Message
                }.ToString());
            }

            if (exc is StudentAlreadyExistsException)
            {
                return context.Response.WriteAsync(new ErrorDetails
                {
                    StatusCode = (int)StatusCodes.Status400BadRequest,
                    Message = exc.Message
                }.ToString());
            }

            return context.Response.WriteAsync(new ErrorDetails
            {
                StatusCode = (int)StatusCodes.Status500InternalServerError,
                Message = "Wystąpił jakiś błąd..."
            }.ToString());
        }
    }
}
