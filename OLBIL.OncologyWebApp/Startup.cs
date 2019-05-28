using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OLBIL.OncologyData;
using System.Reflection;
using AutoMapper;
using MediatR;
using MediatR.Pipeline;
using OLBIL.OncologyApplication.Infrastructure.AutoMapper;
using System;
using OLBIL.OncologyWebApp.Filters;
using OLBIL.OncologyApplication.OncologyPatients.Queries;
using OLBIL.OncologyApplication.Interfaces;
using System.IO;
using OLBIL.Common;
using OLBIL.OncologyInfrastructure;
using OLBIL.OncologyApplication.Infrastructure;
using OLBIL.OncologyCrossCutting;

namespace OLBIL.OncologyWebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add AutoMapper
            services.AddAutoMapper(new Assembly[] { typeof(AutoMapperProfile).GetTypeInfo().Assembly });

            services.AddSingleton(typeof(IDateTimeProvider), typeof(SystemDateTime));

            // Add MediatR
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
            services.AddMediatR(typeof(GetOncologyPatientsListQuery).GetTypeInfo().Assembly);
            services.AddSingleton<IExcelFileExporter, ExcelFileExporter>();
            services.AddSingleton<IDateTimeCalculationsDomainService, DateTimeCalculationsDomainService>();

            services.AddDbContext<IOncologyContext, OncologyContext>(
                    options => options.UseNpgsql(Configuration.GetConnectionString("ElephantDB"))
                );

            services
                .AddMvc(opt => opt.Filters.Add(typeof(OlbilExceptionFilterAttribute)))
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerDocument(config => 
                config.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "OLBIL Oncology API";
                    document.Info.Description = "The API for the Oncology module of OLBIL";
                    document.Info.TermsOfService = "None";
                    document.Info.Contact = new NSwag.SwaggerContact
                    {
                        Name = "Juan Carlos Espinoza",
                        Email = "hello@jcespinoza.com",
                        Url = "https://jcespinoza.com"
                    };
                    document.Info.License = new NSwag.SwaggerLicense
                    {
                        Name = $"Copyright © {DateTime.Now.Year} FOSS Honduras"
                    };
                }
            );

            // Customise default API behavour
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseSwagger();
            app.UseSwaggerUi3(settings =>
            {
                settings.Path = "/api";
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
                }
            });
        }
    }
}
