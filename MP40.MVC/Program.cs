using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using MP40.BLL.Mapping;
using MP40.BLL.Services;
using MP40.DAL.DataBaseContext;
using MP40.DAL.Models;
using MP40.DAL.Repositories;
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

			builder.Services
				.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
				.AddCookie();
			builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

			builder.Services.AddDbContext<RwaMoviesContext>(options =>
				{ options.UseSqlServer("Name=ConnectionStrings:DefaultConnection"); })
				.AddScoped<IRepositoryCollection, RepositoryCollection>(options =>
					{
						return new RepositoryCollection(options.GetRequiredService<RwaMoviesContext>(),
					new Dictionary<Type, Func<RwaMoviesContext, RepositoryCollection, IRepository>>()
					{
						[typeof(User)] = (dbContext, _) => new UserRepository(dbContext),
						[typeof(Video)] = (dbContext, repositoryCollection) => new VideoRepository(dbContext, repositoryCollection) 
					}); 
					});

			builder.Services.AddScoped<BllMapperProfile>()
				.AddScoped<IBijectiveMapper<BllMapperProfile>, BijectiveMapper<BllMapperProfile>>()
				.AddScoped<MvcMapperProfile>()
				.AddScoped<IBijectiveMapper<MvcMapperProfile>, BijectiveMapper<MvcMapperProfile>>();

			builder.Services.AddScoped<IDataService, DataService>();

            #region HashFunction

            static byte[] hashFunction(string password, byte[] salt) =>
             KeyDerivation.Pbkdf2(
                    password: password,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 100000,
                    numBytesRequested: 256 / 8);

            #endregion

            builder.Services.AddSingleton(options =>
				{ return new SecurityService.HashFunction(hashFunction); })
				.AddScoped<ISecurityService, SecurityService>()
				.AddScoped<IAuthenticationService, AuthenticationService>();

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
			application.UseAuthentication();
			application.UseAuthorization();
			application.MapControllerRoute(
				name: "default",
				pattern: "{controller=Login}/{action=Index}/{id?}");

			application.Run();

		}
	}
}
