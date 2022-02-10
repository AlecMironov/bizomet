using System.Diagnostics;
using System.Text.Encodings.Web;
using MailKitMailer.Interfaces;
using MailKitMailer.Models;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MailKitMailer.MailEngine
{
	/// <summary>
	/// View Engine for MailKitMailer
	/// </summary>
	/// <seealso cref="Microsoft.AspNetCore.Mvc.Razor.RazorViewEngine" />
	/// <seealso cref="MailKitMailer.Interfaces.IMailerViewEngine" />
	public class MailerViewEngine : RazorViewEngine, IMailerViewEngine
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="MailerViewEngine"/> class.
		/// </summary>
		/// <param name="pageFactory">The page factory.</param>
		/// <param name="pageActivator">The page activator.</param>
		/// <param name="htmlEncoder">The HTML encoder.</param>
		/// <param name="optionsAccessor">The options accessor.</param>
		/// <param name="loggerFactory">The logger factory.</param>
		/// <param name="diagnosticListener">The diagnostic listener.</param>
		public MailerViewEngine(
			IRazorPageFactoryProvider pageFactory,
			IRazorPageActivator pageActivator,
			HtmlEncoder htmlEncoder,
			IOptions<MailerViewEngineOptions> optionsAccessor,
			ILoggerFactory loggerFactory,
			DiagnosticListener diagnosticListener)
		: base(pageFactory, pageActivator, htmlEncoder, optionsAccessor, loggerFactory, diagnosticListener)
		{
		}
	}
}
