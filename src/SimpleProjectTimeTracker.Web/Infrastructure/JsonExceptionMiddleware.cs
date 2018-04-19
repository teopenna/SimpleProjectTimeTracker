using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace SimpleProjectTimeTracker.Web.Infrastructure
{
    public sealed class JsonExceptionMiddleware
    {
        public const string DefaultErrorMessage = "A server error occurred.";

        private readonly IHostingEnvironment _environment;
        private readonly JsonSerializer _serializer;

        public JsonExceptionMiddleware(IHostingEnvironment environment)
        {
            _environment = environment;

            _serializer = new JsonSerializer
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
        }

        public async Task Invoke(HttpContext context)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            var ex = context.Features.Get<IExceptionHandlerFeature>()?.Error;
            if (ex == null) return;

            var error = BuildError(ex, _environment);

            using (var writer = new StreamWriter(context.Response.Body))
            {
                _serializer.Serialize(writer, error);
                await writer.FlushAsync().ConfigureAwait(false);
            }
        }

        private ApiError BuildError(Exception ex, IHostingEnvironment environment)
        {
            var error = new ApiError();

            if (environment.IsDevelopment())
            {
                error.Message = ex.Message;
                error.Detail = ex.StackTrace;
            }
            else
            {
                error.Message = DefaultErrorMessage;
                error.Detail = ex.Message;
            }

            return error;
        }
    }
}
