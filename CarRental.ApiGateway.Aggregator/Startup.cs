using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRental.ApiGateway.Aggregator.Configurations;
using CarRental.ApiGateway.Aggregator.Controllers.Models.Cars;
using CarRental.ApiGateway.Aggregator.Controllers.Models.Rentals;
using CarRental.ApiGateway.Aggregator.Controllers.Requests;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;

namespace CarRental.ApiGateway.Aggregator
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IServicesEndpointsConfiguration>(Configuration.GetSection("ServicesEndpoints")
                .Get<ServicesEndpointsConfiguration>(c => c.BindNonPublicProperties = true));

            services.AddControllers()
                .AddNewtonsoftJson();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CarRental.ApiGateway.Aggregator", Version = "v1" });
            });

            services.AddAutoMapper(config =>
            {
                config.CreateMap<JsonPatchDocument<CarUpdateRequest>, JsonPatchDocument<RentalCarUpdate>>();
                config.CreateMap<Operation<CarUpdateRequest>, Operation<RentalCarUpdate>>();
                
                config.CreateMap<JsonPatchDocument<CarUpdateRequest>, JsonPatchDocument<CarUpdate>>();
                config.CreateMap<Operation<CarUpdateRequest>, Operation<CarUpdate>>();
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CarRental.ApiGateway.Aggregator v1"));
            }

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
