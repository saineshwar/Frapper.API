using Frapper.API.Helpers;
using Frapper.Common;
using Frapper.Repository;
using Frapper.Repository.Movies.Queries;
using Frapper.Repository.UserMaster.Queries;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Frapper.API
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
            #region Registering Services
            services.AddControllers().AddNewtonsoftJson(options =>
              {
                  options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                  options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                  options.SerializerSettings.ContractResolver =
                      new Newtonsoft.Json.Serialization.DefaultContractResolver();
              });

            var connection = Configuration.GetConnectionString("DatabaseConnection");
            services.AddDbContext<FrapperDbContext>(options => options.UseSqlServer(connection));
            services.Configure<AppSettings>(Configuration.GetSection("ApplicationSettings"));
            services.Configure<ConnectionStrings>(Configuration.GetSection("ConnectionStrings"));

            services.AddTransient<IUserMasterQueries, UserMasterQueries>();
            services.AddTransient<IUserTokensQueries, UserTokensQueries>();
            services.AddTransient<IMoviesQueries, MoviesQueries>();
            services.AddTransient<IUnitOfWorkEntityFramework, UnitOfWorkEntityFramework>();
            services.AddTransient<IUnitOfWorkDapper, UnitOfWorkDapper>();


            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                            {
                                context.Response.Headers.Add("Token-Expired", "true");
                            }
                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddApiVersioning(x =>
            {
                x.DefaultApiVersion = new ApiVersion(1, 0);
                x.AssumeDefaultVersionWhenUnspecified = true;
                x.ReportApiVersions = true;
                x.ApiVersionReader = new HeaderApiVersionReader("x-frapper-api-version");
            });

            services.AddVersionedApiExplorer(
                options =>
                {
                    // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
                    // note: the specified format code will format the version as "'v'major[.minor][-status]"
                    options.GroupNameFormat = "'v'VVV";
                    // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                    // can also be used to control the format of the API version in route templates
                    options.SubstituteApiVersionInUrl = true;
                });

            #endregion

            var contact = new OpenApiContact()
            {
                Name = "Frapper APIS",
                Email = "Frapper@Frapper.com",
                Url = new Uri("https://tutexchange.com/")
            };

            var license = new OpenApiLicense()
            {
                Name = "Frapper License",
                Url = new Uri("https://tutexchange.com/")
            };

            var info1 = new OpenApiInfo()
            {
                Version = "v1",
                Title = "Welcome to Frapper API Version v1",
                Description = "World of Frapper to Learn",
                TermsOfService = new Uri("https://tutexchange.com/"),
                Contact = contact,
                License = license
            };

            var info2 = new OpenApiInfo()
            {
                Version = "v2",
                Title = "Welcome to Frapper API Version v2",
                Description = "World of Frapper to Learn",
                TermsOfService = new Uri("https://tutexchange.com/"),
                Contact = contact,
                License = license
            };

            services.AddSwaggerGen(
                options =>
                {
                    // add a custom operation filter which sets default values
                    //options.OperationFilter<SwaggerDefaultValues>();
                    // integrate xml comments
                    options.IncludeXmlComments(XmlCommentsFilePath);
                    options.SwaggerDoc("v1", info1);
                    options.SwaggerDoc("v2", info2);
                    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header,
                        Description = "Please insert JWT with Bearer into field",
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey
                    });
                    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                         new string[] { }
                       }
                      });
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, 
            IApiVersionDescriptionProvider provider, ILogger<Startup> logger)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            app.UseExceptionMiddleware();

            app.UseHttpsRedirection();
            app.UseRouting();

            // global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.DefaultModelsExpandDepth(-1);

                c.SwaggerEndpoint("/swagger/v1/swagger.json",
                    "Frapper API v1");
                c.SwaggerEndpoint("/swagger/v2/swagger.json",
                    "Frapper API v2");

            });



            app.UseStaticFiles();

            app.UseMiddleware<ValidateHeaderMiddleware>();

            app.UseAuthentication();

            app.UseAuthorization();

            //Add our new middleware to the pipeline
            app.UseMiddleware<RequestResponseLoggingMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static string XmlCommentsFilePath
        {
            get
            {
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var fileName = typeof(Startup).GetTypeInfo().Assembly.GetName().Name + ".xml";
                return Path.Combine(basePath, fileName);
            }
        }
    }
}
