using System;
using System.Reflection;
using Domain.Aggregates.AirportAggregate;
using FluentValidation.AspNetCore;
using Infrastructure;
using Infrastructure.Repositores;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MediatR;
using Domain.Aggregates.FlightAggregate;
using Domain.Aggregates.OrderAggregate;
using API.Middleware;
using Domain.Events;
using Serilog;

namespace API
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
            services.AddSwaggerDocument(config =>
            {
                config.PostProcess = document =>
                {
                    document.Info.Title = "AcmeFlights API";
                    document.Info.Description = "<li>A simple filght booking API implemented using DDD </li>" +
                    "<li> Application logs are captured using Serilog, Seq and available at - <a href=\"http://localhost:9000 \" target=\"_blank\">http://localhost:9000 </a></li>" +
                    "<li>High level design and assumtions made for the implementation are available at - - <a href=\"https://github.com/infaz98/p1-v1-be-dotnet-assignment/blob/feature/flight-booking-engine-infaz/DESIGN-DOCUMENT.md \" target=\"_blank\">Design Document</a></li><br>";
                    document.Info.Contact = new NSwag.OpenApiContact
                    {
                        Name = "Infaz Rumy",
                        Url = "https://linktr.ee/infazrumy"
                    };
                };
            });

            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
                options.ReportApiVersions = true;
            });

            services.AddVersionedApiExplorer(
                options =>
                {
                    options.GroupNameFormat = "'v'VVV";
                    options.SubstituteApiVersionInUrl = true;
                });

            services.AddEndpointsApiExplorer();

            services.AddControllers()
                .AddNewtonsoftJson()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssembly(typeof(Startup).Assembly));
            
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddMediatR(typeof(Startup));

            //registering domain assembly for managing domain events 
            services.AddMediatR(typeof(FlightBookingEvent).GetTypeInfo().Assembly);

            services.AddFlightsContext(
                Configuration["Database:ConnectionString"],
                typeof(Startup).GetTypeInfo().Assembly.GetName().Name);


            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IFlightRepository, FlightRepository>();
            services.AddScoped<IAirportRepository, AirportRepository>();

            services.AddTransient<ExceptionHandlingMiddleware>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSerilogRequestLogging();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.UseOpenApi();
            app.UseSwaggerUi3();
        }
    }
}
