using MailKitMailer.Models;

namespace MailKitMailer.Interfaces
{
	/// <summary>
	/// Mailer Context
	/// </summary>
	public interface IMailerContext
	{
		/// <summary>
		/// Gets the default recipients.
		/// </summary>
		/// <value>
		/// The default recipients.
		/// </value>
		IList<EmailAddressModel> DefaultRecipients { get; }

		/// <summary>
		/// Gets the default cc recipients.
		/// </summary>
		/// <value>
		/// The default cc recipients.
		/// </value>
		IList<EmailAddressModel> DefaultCCRecipients { get; }

		/// <summary>
		/// Gets the default BCC recipients.
		/// </summary>
		/// <value>
		/// The default BCC recipients.
		/// </value>
		IList<EmailAddressModel> DefaultBCCRecipients { get; }

		/// <summary>
		/// Gets or sets from address for whole mailing Context.
		/// </summary>
		/// <value>
		/// From.
		/// </value>
		EmailAddressModel From { get; set; }

		/// <summary>
		/// Called when [before send].
		/// </summary>
		/// <param name="serviceProvider">The service provider.</param>
		void OnBeforeSend(IServiceProvider serviceProvider);

		/// <summary>
		/// Called when [after send].
		/// </summary>
		/// <param name="serviceProvider">The service provider.</param>
		void OnAfterSend(IServiceProvider serviceProvider);
	}
}
