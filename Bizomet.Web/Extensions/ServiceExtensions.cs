using System.Text;
using Bizomet.Data;
using Bizomet.Data.DataEncryption;
using Bizomet.Data.DataEncryption.Providers;
using Bizomet.Data.Entities;
using Bizomet.Web.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Bizomet.Web.Extensions
{
	public static class ServiceExtensions
	{
		public static void ConfigureCors(this IServiceCollection services) => services.AddCors(options =>
		{
			options.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin()
				.AllowAnyMethod()
				.AllowAnyHeader());
		});

		public static void ConfigureDataEncryption(this IServiceCollection services)
		{
			var key = Convert.FromBase64String("ztcJaQV8JYok1HQ9GJUvMg==");
			var iv = Convert.FromBase64String("R+cE+5EhP58cgS5UouchvQ==");
			services.AddSingleton<IEncryptionProvider>(x => ActivatorUtilities.CreateInstance<AesProvider>(x, key, iv));
		}

		public static void ConfigureJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
		{
			var jwtSettings = configuration.GetSection("JwtSettings");
			services.AddAuthentication(opt =>
			{
				opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters {
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,

					ValidIssuer = jwtSettings.GetSection("validIssuer").Value,
					ValidAudience = jwtSettings.GetSection("validAudience").Value,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.GetSection("securityKey").Value))
				};
			});
			services.AddScoped<JwtHandler>();
		}

		public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), sqlServerOptions => sqlServerOptions
						.MigrationsAssembly("Bizomet.Data")
						.CommandTimeout(configuration.GetValue<int>("SqlOptions:CommandTimeOut", 600))));

			services.AddDatabaseDeveloperPageExceptionFilter();
			services.AddIdentity<ApplicationUser, ApplicationRole>(config =>
			{
				config.SignIn.RequireConfirmedEmail = true;
				config.SignIn.RequireConfirmedAccount = true;
			})
			.AddEntityFrameworkStores<ApplicationDbContext>()
			.AddDefaultTokenProviders();
		}

		//public static void ConfigureRepositoryManager(this IServiceCollection services) => services.AddScoped<IRepositoryManager, RepositoryManager>();
	}
}
