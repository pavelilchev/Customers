namespace CustomersREST
{
    using CustomersREST.Database.Context;
    using CustomersREST.Services;
    using CustomersREST.Services.Implementations;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Newtonsoft.Json.Serialization;
    using System;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(setupAction =>
            {
                setupAction.ReturnHttpNotAcceptable = true;
            }).AddNewtonsoftJson(setupAction =>
            {
                setupAction.SerializerSettings.ContractResolver =
                   new CamelCasePropertyNamesContractResolver();
            })
            .AddXmlDataContractSerializerFormatters()
            .ConfigureApiBehaviorOptions(setupAction =>
            {
                setupAction.InvalidModelStateResponseFactory = context =>
                {
                    var problemDetailsFactory = context.HttpContext.RequestServices
                    .GetRequiredService<ProblemDetailsFactory>();

                    var problemDetails = problemDetailsFactory.CreateValidationProblemDetails(context.HttpContext, context.ModelState);

                    problemDetails.Detail = "See the errors field for details";
                    problemDetails.Instance = context.HttpContext.Request.Path;

                    var actionExecutingContext = context as Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext;
                    if (context.ModelState.ErrorCount > 0 &&
                        actionExecutingContext?.ActionArguments.Count == context.ActionDescriptor.Parameters.Count)
                    {
                        problemDetails.Type = "https://customers.com/modelvalidationproblem";
                        problemDetails.Status = StatusCodes.Status422UnprocessableEntity;
                        problemDetails.Title = "One or more validation errors occurred.";

                        return new UnprocessableEntityObjectResult(problemDetails)
                        {
                            ContentTypes = { "application/problem+json" }
                        };
                    }

                    problemDetails.Status = StatusCodes.Status400BadRequest;
                    problemDetails.Title = "One or more errors on input occurred.";
                    return new BadRequestObjectResult(problemDetails)
                    {
                        ContentTypes = { "application/problem+json" }
                    };
                };
            });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<ICustomersRepository, CustomersRepository>();

            services.AddDbContext<CustomersContext>(options =>
            {
                options.UseSqlServer(this.Configuration.GetConnectionString("CustomersConnectionString")).EnableSensitiveDataLogging();
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("An unexpected fault happened. Try again later.");
                    });
                });
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
