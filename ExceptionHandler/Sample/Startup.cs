using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using ExceptionHandler;
using ExceptionHandler.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sample.ExceptionHandlers;
using Sample.Services;

namespace Sample
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddScoped<LoggerService, LoggerService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseExceptionMiddleware().Catch<InvalidAsynchronousStateException>()
                                        .AndReturnAsync((context, exception, serviceProvider) => Task.FromResult(new Response(HttpStatusCode.AlreadyReported, $"The path {context.Request.Path} failed with {exception.Message}")))
                                   
                                        .Catch<FileNotFoundException>()
                                        .AndReturnAsync(HttpStatusCode.NotFound, "There was error with retriving file. Please try again later")
                                      
                                        .Catch<IndexOutOfRangeException>()
                                        .AndCall<IndexOutOfRangeExceptionHandler>()
                
                                        .CatchDefault()
                                        .AndCall(() => new DefaultExceptionHandler());

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
