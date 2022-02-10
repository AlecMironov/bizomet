using System.Reflection;
using MailKit.Net.Smtp;
using MailKitMailer.Interfaces;
using MailKitMailer.MailEngine;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MailKitMailer
{
	/// <summary>
	/// MailKitMailerExtensions
	/// </summary>
	public static class MailKitMailerExtensions
	{
		/// <summary>
		/// Adds the ASP net core mail kit mailer.
		/// </summary>
		/// <param name="services">The services.</param>
		/// <param name="configuration">The configuration.</param>
		/// <param name="configureClient">Action to configure the smtp client defaults.</param>
		/// <returns></returns>
		public static IServiceCollection ConfigureMailKitMailer(this IServiceCollection services, IConfiguration configuration, Action<SmtpClient> configureClient = null)
		{
			services = CheckForHttpClient(services);
			services.Configure<Models.SMTPConfigModel>(x => configuration.GetSection("MailerConfiguration").Bind(x));
			services.Configure<Models.MailerViewEngineOptions>(x => x = new Models.MailerViewEngineOptions());
			services.AddScoped<IMailerViewEngine, MailerViewEngine>();
			services.AddScoped<IMailClient, MailClient>();
			services.AddScoped<IMailKitSMTPClient>(x =>
			{
				MailKitSMTPClient client = new MailKitSMTPClient();

				if (configureClient != null) {
					configureClient(client);
				}

				return client as IMailKitSMTPClient;
			});
			return services;
		}

		/// <summary>
		/// Adds the ASP net core mail kit mailer.
		/// </summary>
		/// <param name="services">The services.</param>
		/// <param name="smtpconfig">The smtpconfig.</param>
		/// <param name="configureClient">The configure client.</param>
		/// <returns></returns>
		public static IServiceCollection ConfigureMailKitMailer(this IServiceCollection services, Models.SMTPConfigModel smtpconfig, Action<SmtpClient> configureClient = null)
		{
			services = CheckForHttpClient(services);
			services.Configure<Models.SMTPConfigModel>(x =>
			{
				x.GetType().GetProperties()
				.ToList().ForEach(p =>
				{
					if (smtpconfig.GetType().GetProperty(p.Name) != null) {
						p.SetValue(x, smtpconfig.GetType().GetProperty(p.Name).GetValue(smtpconfig));
					}
				});
			});
			services.Configure<Models.MailerViewEngineOptions>(x => x = new Models.MailerViewEngineOptions());
			services.AddScoped<IMailerViewEngine, MailerViewEngine>();
			services.AddScoped<IMailClient, MailClient>();
			services.AddScoped<IMailKitSMTPClient>(x =>
			{
				MailKitSMTPClient client = new MailKitSMTPClient();

				if (configureClient != null) {
					configureClient(client);
				}

				return client as IMailKitSMTPClient;
			});

			return services;
		}

		/// <summary>
		/// Adds the ASP net core mail kit mailer.
		/// </summary>
		/// <param name="services">The services.</param>
		/// <param name="fromAddress">From address.</param>
		/// <param name="fromName">From name.</param>
		/// <param name="host">The host.</param>
		/// <param name="username">The username.</param>
		/// <param name="password">The password.</param>
		/// <param name="port">The port.</param>
		/// <param name="UseSSL">if set to <c>true</c> [use SSL].</param>
		/// <returns></returns>
		public static IServiceCollection ConfigureMailKitMailer(this IServiceCollection services, string fromAddress, string fromName, string host, string username, string password, int port, bool UseSSL, Action<SmtpClient> configureClient = null)
		{
			services = CheckForHttpClient(services);
			services.Configure<Models.SMTPConfigModel>(x => x = new Models.SMTPConfigModel() {
				FromAddress = new Models.EmailAddressModel() {
					Name = fromName,
					Email = fromAddress,

				},
				Host = host,
				Port = port,
				UseSSL = UseSSL,
				Username = username,
				Password = password
			});
			services.Configure<Models.MailerViewEngineOptions>(x => x = new Models.MailerViewEngineOptions());
			services.AddScoped<IMailerViewEngine, MailerViewEngine>();
			services.AddScoped<IMailClient, MailClient>();
			services.AddScoped<IMailKitSMTPClient>(x =>
			{
				MailKitSMTPClient client = new MailKitSMTPClient();

				if (configureClient != null) {
					configureClient(client);
				}

				return client as IMailKitSMTPClient;
			});

			return services;
		}

		/// <summary>
		/// Registers all mail Contexts.
		/// </summary>
		/// <param name="services">The services.</param>
		/// <returns></returns>
		public static IServiceCollection RegisterAllMailContextOfCallingAssembly(this IServiceCollection services)
		{
			return RegisterAllMailContextsOfAssembly(services, Assembly.GetCallingAssembly());
		}

		/// <summary>
		/// Registers the type of all mail Contexts of assembly containing given type.
		/// </summary>
		/// <typeparam name="TContaining">The type of the containing.</typeparam>
		/// <param name="services">The services.</param>
		/// <returns></returns>
		public static IServiceCollection RegisterAllMailContextOfAssemblyContainingType<TContaining>(this IServiceCollection services)
		{
			return RegisterAllMailContextsOfAssembly(services, Assembly.GetAssembly(typeof(TContaining)));
		}

		/// <summary>
		/// Registers all mail Contexts of assembly.
		/// </summary>
		/// <param name="services">The services.</param>
		/// <param name="assembly">The assembly.</param>
		/// <returns></returns>
		public static IServiceCollection RegisterAllMailContextsOfAssembly(this IServiceCollection services, Assembly assembly)
		{
			var q = assembly.GetTypes().Where(x => !x.IsAbstract && x.IsSubclassOf(typeof(MailerContextAbstract))).ToList();

			foreach (var ContextCls in q) {
				var interfaces = ContextCls.GetInterfaces();

				if (interfaces.Any(x => x != typeof(IMailerContext) && typeof(IMailerContext).IsAssignableFrom(x))) {
					var desiredInterface = interfaces.First(x => x != typeof(IMailerContext) && typeof(IMailerContext).IsAssignableFrom(x));
					services.AddScoped(desiredInterface, ContextCls);
				}
				else {
					services.AddScoped(ContextCls);
				}
			}

			return services;
		}

		private static IServiceCollection CheckForHttpClient(IServiceCollection services)
		{
			if (!services.Any(x => x.ServiceType == typeof(IHttpClientFactory))) {
				services.AddHttpClient();
			}

			return services;
		}
	}
}
