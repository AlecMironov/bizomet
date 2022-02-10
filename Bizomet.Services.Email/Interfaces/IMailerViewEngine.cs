using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace MailKitMailer.Interfaces
{
	/// <summary>
	/// Mailer View Engine
	/// </summary>
	/// <seealso cref="Microsoft.AspNetCore.Mvc.Razor.IRazorViewEngine" />
	/// <seealso cref="Microsoft.AspNetCore.Mvc.ViewEngines.IViewEngine" />
	public interface IMailerViewEngine : IRazorViewEngine, IViewEngine
	{
	}
}
