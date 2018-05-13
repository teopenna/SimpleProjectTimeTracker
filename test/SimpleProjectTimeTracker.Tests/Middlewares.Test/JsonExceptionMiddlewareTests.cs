using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using SimpleProjectTimeTracker.Web.Infrastructure;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace SimpleProjectTimeTracker.Tests.Middlewares.Test
{
    public class JsonExceptionMiddlewareTests
    {
        [Fact]
        public async Task JsonExceptionMiddleware()
        {
            var hostBuilder = new WebHostBuilder()
                .Configure(app =>
                {
                    var jsonExceptionMiddleware = new JsonExceptionMiddleware(app.ApplicationServices.GetRequiredService<IHostingEnvironment>());
                    app.UseExceptionHandler(new ExceptionHandlerOptions { ExceptionHandler = jsonExceptionMiddleware.Invoke });
                    app.Run(async context =>
                    {
                        var customExceptionHandlerFeature = new ExceptionHandlerFeature()
                        {
                            Error = new Exception("Test exception"),
                            Path = "/"
                        };
                        context.Features.Set<IExceptionHandlerFeature>(customExceptionHandlerFeature);
                        await context.Response.WriteAsync("Test response");
                    });
                });

            using (var server = new TestServer(hostBuilder))
            {
                var response = await server.CreateRequest("/")
                    .GetAsync();

                var body = await response.Content.ReadAsStringAsync();

                Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
            }
        }
    }
}
