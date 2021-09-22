using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using CompanyName.GatewayManagement.Api.DI;
using CompanyName.GatewayManagement.Api.Filters;
using CompanyName.GatewayManagement.Domain;
using CompanyName.GatewayManagement.Domain.DTO;
using System;
using System.IO;
using System.Reflection;

namespace CompanyName.GatewayManagement.Api
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
            services.AddAutoMapper(typeof(Startup));
            services.AddControllers();
            services.AddMvc(options =>
            {
                options.Filters.Add<GatewayManagementExceptionFilterAttribute>();
            });

            var dependencyInjection = new DependencyInjection(services);
            dependencyInjection.InjectAll();

            services.AddAutoMapper(Assembly.Load("CompanyName.GatewayManagement.Domain"));
            services.AddAutoMapper(Assembly.Load("CompanyName.GatewayManagement.Data"));

            services.ConfigureSqlServerContext(Configuration);
            // Add functionality to inject IOptions<T>
            services.AddOptions();
            // Add ApplicationSettings object so it can be injected
            services.Configure<ApplicationSettings>(Configuration.GetSection("ApplicationSettings"));
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.GetEnumerator().Current);
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Gateway Management API",
                    Description = "Gateway Management API",
                    TermsOfService = new Uri(DomainConstants.SWAGGER_TERMS_OF_SERVICE_URL),
                    Contact = new OpenApiContact
                    {
                        Name = "CompanyName",
                        Email = string.Empty,
                        Url = new Uri(DomainConstants.SWAGGER_CONTRACT_URL),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new Uri(DomainConstants.SWAGGER_CONTRACT_URL),
                    }
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwagger(c =>
            {
                c.RouteTemplate = "swagger/{documentName}/swagger.json";
            });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("../swagger/v1/swagger.json", "Gateway Management Api");
                c.DocumentTitle = "Gateway Management Api";
                c.RoutePrefix = "swagger";
            });
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
