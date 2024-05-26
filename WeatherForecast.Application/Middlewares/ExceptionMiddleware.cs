using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Diagnostics;
using WeatherForecast.Domain.Enums;
using WeatherForecast.Domain.Exceptions;
using WeatherForecast.DTO;

namespace WeatherForecast.Application.Middlewares
{
    public class ExceptionMiddleware
    {
        private const string JsonContentType = "application/json";

        private JsonSerializerSettings SerializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        private readonly RequestDelegate request;

        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            request = next;
            _logger = logger;
        }

        public Task Invoke(HttpContext context)
        {
            return InvokeAsync(context);
        }

        private async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await request(context);
            }
            catch (WeatherAPIException ex)
            {
                string operationId2 = Activity.Current?.TraceId.ToString();
                ErrorResponse response3 = new ErrorResponse(ex.Message, null, ex.AdditionalInfo);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)ex.ErrorCode;
                await context.Response.WriteAsync(JsonConvert.SerializeObject(response3, SerializerSettings));
                _logger.LogError(ex.Message, ex.ErrorCode, operationId2);
            }
            catch (BadHttpRequestException ex4) when (ex4.StatusCode == 413)
            {
                string operationId4 = Activity.Current?.TraceId.ToString();
                ErrorResponse response4 = new ErrorResponse(ex4.Message, operationId4);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = ex4.StatusCode;
                await context.Response.WriteAsync(JsonConvert.SerializeObject(response4, SerializerSettings));
                _logger.LogError(ex4.Message, ErrorCode.LargeRequest, operationId4);
            }
            catch (DbUpdateConcurrencyException ex3)
            {
                string operationId3 = Activity.Current?.TraceId.ToString();
                ErrorResponse response2 = new ErrorResponse("An error occurred while running the operation. Another user or process has already modified the data since this operation was started. Please review your changes and try again.", operationId3);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = 409;
                await context.Response.WriteAsync(JsonConvert.SerializeObject(response2, SerializerSettings));
                _logger.LogError(ex3.Message, ErrorCode.Conflict, operationId3);
            }
            catch (Exception ex2)
            {
                string operationId = Activity.Current?.TraceId.ToString();
                ErrorResponse response = new ErrorResponse("Unhandled Server Error", operationId);
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(response, SerializerSettings));
                _logger.LogError(ex2.Message, ErrorCode.Internal, ex2.Message);
            }
        }
    }
}
