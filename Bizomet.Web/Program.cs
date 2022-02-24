using Bizomet.Web.Extensions;
using MailKitMailer;
using Microsoft.AspNetCore.HttpOverrides;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, lc) => lc
	.WriteTo.Console()
	.ReadFrom.Configuration(ctx.Configuration));

// Add services to the container.
builder.Services.ConfigureDataEncryption();
builder.Services.ConfigureSqlContextAndIdentity(builder.Configuration);
builder.Services.ConfigureAutoMapper();
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureJwtAuthentication(builder.Configuration);
builder.Services.ConfigureWebsiteCookies(builder.Configuration);
builder.Services.ConfigureMailKitMailer(builder.Configuration)
	.RegisterAllMailContextOfCallingAssembly();

//builder.Services.AddCors();
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllersWithViews();
builder.Services.AddControllers();

builder.Services.ConfigureSwagger();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
	app.UseMigrationsEndPoint();
	app.UseSwagger();
	app.UseSwaggerUI();
}
else {
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
// global cors policy
//app.UseCors(x => x
//	.AllowAnyMethod()
//	.AllowAnyHeader()
//	.SetIsOriginAllowed(origin => true) // allow any origin
//	.AllowCredentials()); // allow credentials

app.UseForwardedHeaders(new ForwardedHeadersOptions { ForwardedHeaders = ForwardedHeaders.All });

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller}/{action=Index}/{id?}");
app.MapFallbackToFile("index.html"); ;

app.Run();
