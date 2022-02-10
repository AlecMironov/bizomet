using MailKit.Net.Smtp;

namespace MailKitMailer.Interfaces
{
	/// <summary>
	/// This interface is used to separate the smtp client from other registered instances
	/// </summary>
	/// <seealso cref="MailKit.Net.Smtp.ISmtpClient" />
	public interface IMailKitSMTPClient : ISmtpClient
	{
	}
}
