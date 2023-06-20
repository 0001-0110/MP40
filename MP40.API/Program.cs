using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MP40.BLL.Mapping;
using MP40.BLL.Services;
using MP40.DAL.DataBaseContext;
using MP40.DAL.Repositories;
using System.Text;

namespace MP40
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Configure JWT services
            builder.Services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    string jwtKey = builder.Configuration["JWT:Key"];
                    byte[] jwtKeyBytes = Encoding.UTF8.GetBytes(jwtKey);
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = builder.Configuration["JWT:Issuer"],
                        ValidateAudience = true,
                        ValidAudience = builder.Configuration["JWT:Audience"],
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(jwtKeyBytes),
                        ValidateLifetime = true,
                    };
                });

            // https://www.c-sharpcorner.com/article/how-to-add-jwt-bearer-token-authorization-functionality-in-swagger/
            builder.Services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "JWTToken_Auth_API",
                    Version = "v1"
                });
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
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

            builder.Services.AddDbContext<RwaMoviesContext>(options =>
                { options.UseSqlServer("Name=ConnectionStrings:DefaultConnection"); });
            builder.Services.AddScoped<IRepositoryCollection, RepositoryCollection>();
            builder.Services.AddScoped<BllMapperProfile>();
            builder.Services.AddScoped<IBijectiveMapper<BllMapperProfile>, BijectiveMapper<BllMapperProfile>>();
            builder.Services.AddScoped<IDataService, DataService>();
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

            WebApplication application = builder.Build();

            // Configure the HTTP request pipeline.
            if (application.Environment.IsDevelopment())
            {
                application.UseSwagger();
                application.UseSwaggerUI();
            }

            application.UseHttpsRedirection();
            application.UseAuthorization();
            application.MapControllers();

            application.Run();
        }
    }
}
