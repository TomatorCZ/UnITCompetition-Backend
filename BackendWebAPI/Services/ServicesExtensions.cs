using BackendWebAPI.Exceptions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace BackendWebAPI.Services
{

    public static class ServicesExtensions
    {
        public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
        {
            builder.Services
                //.AddDummyHardwareDeviceReceiver()

                // Database
                .AddDbContext<CommonDbContext>(options =>
                    options.UseSqlite($"Data Source=DB.db"))

                // Singletons
                .AddSingleton<ExceptionResolver>()

                // Scoped
                //.AddScoped<TelemetryDataRepository>()
                //.AddScoped<TelemetryDataMapper>()

                // Controler
                .AddControllers();

            return builder;
        }

        public static WebApplicationBuilder AddSwagger(this WebApplicationBuilder builder)
        {
            builder.Services
                .AddSwaggerGen(options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Title = "UnIT API",
                        Version = "v1"
                    });

                    options.AddSecurityDefinition("jwt_auth", new OpenApiSecurityScheme
                    {
                        Name = "Bearer",
                        BearerFormat = "JWT",
                        Scheme = "bearer",
                        Description = "JWT with Bearer",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.Http
                    });

                    options.AddSecurityRequirement(
                        new OpenApiSecurityRequirement()
                        {
                    {
                        new OpenApiSecurityScheme()
                        {
                            Reference = new OpenApiReference()
                            {
                                Id = "jwt_auth",
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        new string[] { }},
                        });

                    //var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    //options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
                });
            return builder;
        }

        public static WebApplicationBuilder AddIdentity(this WebApplicationBuilder builder)
        {
            builder.Services
                .AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<CommonDbContext>()
                .AddDefaultTokenProviders();

            return builder;
        }

        public static WebApplicationBuilder AddSecurity(this WebApplicationBuilder builder)
        {
            builder.Services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration.GetSection("JWT")["ValidAudience"],
                    ValidIssuer = builder.Configuration.GetSection("JWT")["ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JWT")["Secret"]))
                };
            });

            return builder;
        }
    }
}
