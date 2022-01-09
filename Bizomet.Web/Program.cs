using System.Text;
using Bizomet.Data.Models;
using Bizomet.Web.Extensions;
using Bizomet.Web.JwtFeatures;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, lc) => lc
	.WriteTo.Console()
	.ReadFrom.Configuration(ctx.Configuration));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.ConfigureCors();
builder.Services.ConfigureIISIntegration();
builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.ConfigureRepositoryManager();
builder.Services.AddAutoMapper(typeof(Bizomet.Web.Mapping.MappingProfile));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(opt =>
{
	opt.Password.RequiredLength = 7;
	opt.Password.RequireDigit = true;
	opt.User.RequireUniqueEmail = true;
})
 .AddEntityFrameworkStores<Bizomet.Data.ApplicationDbContext>();

var jwtSettings = builder.Configuration.GetSection("JwtSettings");
builder.Services.AddAuthentication(opt =>
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

builder.Services.AddScoped<JwtHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
	app.UseDeveloperExceptionPage();
}
else {
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCors("CorsPolicy");

app.UseForwardedHeaders(new ForwardedHeadersOptions {
	ForwardedHeaders = ForwardedHeaders.All
});

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller}/{action=Index}/{id?}");

app.MapRazorPages();
app.MapFallbackToFile("index.html"); ;

app.Run();
