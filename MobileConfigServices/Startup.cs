using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MobileConfigServices.Entities;
using MobileConfigServices.Models;
using MobileConfigServices.Services;
using Newtonsoft.Json.Serialization;
using AutoMapper;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace MobileConfigServices
{
    public class Startup
    {

        private const String _routebookDBconnString = "connectionStrings:RoutebookPTConnection";
        private const String _azureDBconnString = "connectionStrings:AzureMobileConfigDatabase";
        private const String _azureDBconnString2 = "connectionStringsAzureMobileConfigDatabase2";

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appSettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appSettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                //.AddEnvironmentVariables()
                ;

            Configuration = builder.Build();

        }

        public static IConfiguration Configuration { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            try
            {
                services.AddMvc()
                    .AddJsonOptions(o =>
                    {
                        if (o.SerializerSettings.ContractResolver != null)
                        {
                            var castedResolver = o.SerializerSettings.ContractResolver
                                as DefaultContractResolver;
                            castedResolver.NamingStrategy = null;  // Use property names as used in the class.  case in the Model
                    }
                    });

#if RELEASE               
                var connectionString = Configuration[_azureDBconnString];
#elif DEBUG
                var connectionString = Configuration[_routebookDBconnString]; 
#endif
                services.AddDbContext<RoutebookPTContext>(o => o.UseSqlServer(connectionString));
                services.AddScoped<IConfigurationRepository, ConfigurationRepository>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        [Obsolete]
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();
            loggerFactory.AddDebug();

            //loggerFactory.AddProvider(new NLog.Extensions.Logging.NLogLoggerProvider());
            loggerFactory.AddNLog();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseStatusCodePages();



            // Add Mvc middleware to request pipeline
            app.UseMvc();


            Mapper.Initialize(c => 
            {
                c.CreateMap<CfgLocation, LocationDto>();
                c.CreateMap<CfgRoute, RouteDto>();
                c.CreateMap<CfgLocation, LocationRoutesDto>();
                c.CreateMap<CfgParameters, ParametersDto>();

            });

            

            //app.Run((context) =>
            //{
            //    throw new Exception("Example Exception");
            //});

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }
    }
}
