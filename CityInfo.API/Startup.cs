using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;
using NLog.Extensions.Logging;

namespace CityInfo.API
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
            .AddMvcOptions(o=>o.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter()));
            //JsonSerializer settings
            // .AddJsonOptions(o=>{
            //     if (o.SerializerSettings.ContractResolver != null)
            //     {
            //         var castedResolver = o.SerializerSettings.ContractResolver
            //         as DefaultContractResolver; 
            //         castedResolver.NamingStrategy= null;
                    
            //     } 
            // } );
            services.AddTransient<LocalMailService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,ILoggerFactory loggerfactory)
        {
            loggerfactory.AddConsole();
            loggerfactory.AddDebug();
           // loggerfactory.AddProvider(new NLog.Extensions.Logging.NLogLoggerProvider());
           loggerfactory.AddNLog();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
           // app.Run((contex) => { throw new Exception("Example exception"); });
            else
            {
                app.UseHsts();
            }
            //Format status code

            app.UseStatusCodePages();

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
