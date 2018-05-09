using System;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleProjectTimeTracker.Web.Helpers;
using SimpleProjectTimeTracker.Web.Infrastructure;
using SimpleProjectTimeTracker.Web.MappingProfiles;
using SimpleProjectTimeTracker.Web.Services;

namespace SimpleProjectTimeTracker.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SimpleProjectTimeTrackerDbContext>(opt => opt.UseInMemoryDatabase("SimpleProjectTimeTracker"));
            services.AddCors(options => 
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
            services.AddMvc();
            services.AddAutoMapper(x => x.AddProfile(new MappingProfile()));
            services.AddScoped<ITimeRegistrationService, TimeRegistrationService>();
            services.AddScoped<IInvoiceService, InvoiceService>();

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCors("CorsPolicy");
            }

            var jsonExceptionMiddleware = new JsonExceptionMiddleware(app.ApplicationServices.GetRequiredService<IHostingEnvironment>());
            app.UseExceptionHandler(new ExceptionHandlerOptions { ExceptionHandler = jsonExceptionMiddleware.Invoke });

            var context = serviceProvider.GetService<SimpleProjectTimeTrackerDbContext>();
            SampleDataGenerator.SeedData(context);

            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseProxyToSpaDevelopmentServer("http://localhost:3000");
                }
            });
        }
    }
}
