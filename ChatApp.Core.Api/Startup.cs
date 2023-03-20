using ChatApp.Core.Data;
using ChatApp.Core.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ChatApp.Core.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Console.WriteLine("Env===============" + env.EnvironmentName);
            var builder = new ConfigurationBuilder()
                            .SetBasePath(env.ContentRootPath)
                            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                            .AddEnvironmentVariables();

            Configuration = builder.Build();
            GlobalConfig.SetConfiguration(configuration);
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            #region Cors
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });
            #endregion

            #region Database
            services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(Configuration.GetConnectionString("POSTGRESDB")));
            #endregion

            #region Controller json options
            services.AddControllers().AddJsonOptions(x =>
                   {
                       x.JsonSerializerOptions.PropertyNamingPolicy = null;
                   });

            #endregion

            #region JWt Token
            services.AddAuthentication(x =>
            {
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(GlobalConfig.GetConfiguration("Jwt:Key"))),
                    ClockSkew = TimeSpan.Zero
                };
            });
            #endregion

            #region Dependancy Injection
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IDapperRepositiory, DapperRepositiory>();

            #endregion

            #region Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = GlobalConfig.GetConfiguration("Swagger:Title"),
                    Version = GlobalConfig.GetConfiguration("Swagger:Version")
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Description = "Enter the Bearer Authorization string as following: `Bearer Generated-JWT-Token`",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                 {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                 });
            });
            #endregion

            #region Routing
            services.AddRouting(options => options.LowercaseUrls = true);
            #endregion

            #region Payload error configure
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            #endregion

            #region CustomConfig
            services.AddOptions();
            //services.Configure<BkashPaymentGatewayConfig>(Configuration.GetSection("PaymentGatewayConfig:Bkash"));
            #endregion

            #region Memory Cache
            services.AddMemoryCache();
            #endregion

            #region CurrentHost
            services.AddHttpContextAccessor();
            #endregion
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationDbContext dataContext)
        {
            #region Cors
            app.UseCors(x => x
               .SetIsOriginAllowed(origin => true)
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials());

            #endregion

            #region Apply auto migrations
            var migrations = dataContext.Database.GetPendingMigrations().ToList();
            dataContext.Database.Migrate();
            #endregion

            #region Route not found and unautorized Error
            //app.UseStatusCodePages(new StatusCodePagesOptions()
            //{
            //    HandleAsync = async (context) =>
            //    {
            //        var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

            //        if (context.HttpContext.Response.StatusCode == (int)HttpStatusCode.NotFound)
            //        {
            //            var errorDto = ErrorHelper.GetErrorResponse(404, "RouteNotFoundException", "Route not found");

            //            //LoggerHelper.SaveLog(JsonConvert.SerializeObject(errorDto), context.HttpContext);
            //            await context.HttpContext.Response.WriteAsJsonAsync(errorDto, options: options);
            //        }
            //        else if (context.HttpContext.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
            //        {
            //            var errorDto = ErrorHelper.GetErrorResponse(401, "UnAuthorizedException", "You are not loggedIn or not authorized.");

            //            //LoggerHelper.SaveLog(JsonConvert.SerializeObject(errorDto), context.HttpContext);
            //            await context.HttpContext.Response.WriteAsJsonAsync(errorDto, options: options);
            //        }
            //    }
            //});
            #endregion

            #region Swagger
            if (env.IsDevelopment() || env.IsStaging())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ChatApp.Core.Api v1"));
            }
            #endregion

            app.UseHttpsRedirection();

            app.UseRouting();

            //app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}