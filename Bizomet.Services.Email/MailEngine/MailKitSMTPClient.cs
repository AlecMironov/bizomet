using MailKit.Net.Smtp;
using MailKitMailer.Interfaces;

namespace MailKitMailer.MailEngine
{
	/// <summary>
	/// 
	/// </summary>
	/// <seealso cref="MailKit.Net.Smtp.SmtpClient" />
	/// <seealso cref="MailKitMailer.Interfaces.IMailKitSMTPClient" />
	public class MailKitSMTPClient : SmtpClient, IMailKitSMTPClient
	{
	}
}
