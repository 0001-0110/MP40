using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MP40.BLL.Services;
using MP40.DAL.DataBaseContext;
using MP40.DAL.Repositories;
using System.Text;

namespace MP40.API
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

            builder.Services.AddDbContext<RwaMoviesContext>(
                options => { options.UseSqlServer("Name=ConnectionStrings:DefaultConnection"); });
            builder.Services.AddScoped<IRepositoryCollection, RepositoryCollection>();
            builder.Services.AddScoped<IDataService, DataService>();

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
