using Microsoft.EntityFrameworkCore;
using MP40.BLL.Mapping;
using MP40.BLL.Services;
using MP40.DAL.DataBaseContext;
using MP40.DAL.Repositories;
using MP40.MVC.Controllers;
using MP40.MVC.Mapping;

namespace MP40.MVC
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<RwaMoviesContext>(options =>
                { options.UseSqlServer("Name=ConnectionStrings:DefaultConnection"); });
            builder.Services.AddScoped<IRepositoryCollection, RepositoryCollection>();
            builder.Services.AddScoped<BllMapperProfile>();
            builder.Services.AddScoped<IBijectiveMapper<BllMapperProfile>, BijectiveMapper<BllMapperProfile>>();
            builder.Services.AddScoped<MvcMapperProfile>();
            builder.Services.AddScoped<IBijectiveMapper<MvcMapperProfile>, BijectiveMapper<MvcMapperProfile>>();
            builder.Services.AddScoped<IDataService, DataService>();
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

            WebApplication application = builder.Build();

            // Configure the HTTP request pipeline.
            if (!application.Environment.IsDevelopment())
            {
                application.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                application.UseHsts();
            }

            application.UseHttpsRedirection();
            application.UseStaticFiles();
            application.UseRouting();
            application.UseAuthorization();
            application.MapControllerRoute(
                name: "default",
                pattern: "{controller=Login}/{action=Login}/{id?}");

            application.Run();

        }
    }
}
