using System.Text;
using Bizomet.Contracts;
using Bizomet.Data;
using Bizomet.Data.DataEncryption;
using Bizomet.Data.DataEncryption.Providers;
using Bizomet.Data.Entities;
using Bizomet.Repository;
using Bizomet.Web.Helpers;
using Bizomet.Web.Mappings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

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
			var jwtSettings = new JwtSettings();
			configuration.Bind("JwtSettings", jwtSettings);
			services.AddSingleton(jwtSettings);

			services.AddAuthentication(opt =>
			{
				opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
			{
				options.SaveToken = true;
				options.TokenValidationParameters = new TokenValidationParameters {
					ValidateIssuerSigningKey = jwtSettings.ValidateIssuerSigningKey,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.IssuerSigningKey)),
					ValidateIssuer = jwtSettings.ValidateIssuer,
					ValidIssuer = jwtSettings.ValidIssuer,
					ValidateAudience = jwtSettings.ValidateAudience,
					ValidAudience = jwtSettings.ValidAudience,
					RequireExpirationTime = jwtSettings.RequireExpirationTime,
					ValidateLifetime = jwtSettings.RequireExpirationTime,
					ClockSkew = TimeSpan.FromDays(1),
				};
				options.Events = new JwtBearerEvents {
					OnAuthenticationFailed = context =>
					{
						if (context.Exception.GetType() == typeof(SecurityTokenExpiredException)) {
							context.Response.Headers.Add("Token-Expired", "true");
						}
						return Task.CompletedTask;
					}
				};
			});

			services.AddTransient<ITokenService, TokenService>();
		}

		public static void ConfigureSqlContextAndIdentity(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), sqlServerOptions => sqlServerOptions
						.MigrationsAssembly("Bizomet.Data")
						.CommandTimeout(configuration.GetValue<int>("SqlOptions:CommandTimeOut", 600))));

			services.AddDatabaseDeveloperPageExceptionFilter();
			services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
			{
				options.SignIn.RequireConfirmedEmail = true;
				options.SignIn.RequireConfirmedAccount = true;
				options.SignIn.RequireConfirmedPhoneNumber = false; //todo: implement phone confirmation

				// Password settings.
				options.Password.RequireDigit = true;
				options.Password.RequireLowercase = true;
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequireUppercase = true;
				options.Password.RequiredLength = 8;
				options.Password.RequiredUniqueChars = 1;

				// Lockout settings.
				options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
				options.Lockout.MaxFailedAccessAttempts = 5;
				options.Lockout.AllowedForNewUsers = true;
				// User settings.
				options.User.RequireUniqueEmail = false;
			})
			.AddEntityFrameworkStores<ApplicationDbContext>()
			.AddDefaultTokenProviders();
		}

		public static void ConfigureRepositoryManager(this IServiceCollection services) => services.AddScoped<IRepositoryManager, RepositoryManager>();

		public static void ConfigureAutoMapper(this IServiceCollection services) => services.AddAutoMapper(configAction => configAction.AddProfile<MappingProfile>());

		public static void ConfigureWebsiteCookies(this IServiceCollection services, IConfiguration configuration)
		{
			services.ConfigureApplicationCookie(options =>
			{
				// Cookie settings
				options.Cookie.Name = "bizomet.cookie";
				options.Cookie.HttpOnly = true;
				options.SlidingExpiration = true;
				options.ExpireTimeSpan = TimeSpan.FromMinutes(65);
			});
		}

		public static void ConfigureSwagger(this IServiceCollection services)
		{
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			services.AddEndpointsApiExplorer();
			services.AddSwaggerGen(options =>
			{
				options.SwaggerDoc("v1", new OpenApiInfo {
					Version = "v1",
					Title = "Bizomet API",
					Description = "Bizomet Web API"
				});
				options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
					Name = "Authorization",
					Type = SecuritySchemeType.Http,
					Scheme = "Bearer",
					BearerFormat = "JWT",
					In = ParameterLocation.Header,
					Description = "JWT Authorization header using the Bearer scheme"
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
						new string[] {}
					}
				});
			});
		}
	}
}
