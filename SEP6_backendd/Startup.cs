using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Pomelo.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using SEP6_backendd.Models;
using SEP6_backendd.Services;

namespace SEP6_backendd
{
    public class Startup
    {
        public const string AppS3BucketKey = "AppS3Bucket";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IConfiguration Configuration { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // Add S3 to the ASP.NET Core dependency injection framework.
            services.AddAWSService<Amazon.S3.IAmazonS3>();
            services.AddSingleton<IFrequencyService, FrequencyService>();
            services.AddSingleton<IDestinationService, DestinationService>();
            services.AddSingleton<ITrackerService, TrackerService>();
            services.AddSingleton<IWeatherService, WeatherService>();
            services.AddSingleton<IPlanesService, PlaneService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }


    }
}
