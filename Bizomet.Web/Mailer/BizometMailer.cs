using MailKitMailer.MailEngine;
using MailKitMailer.Interfaces;
using MailKitMailer.Models;
using Bizomet.Models.MailModels;

namespace Bizomet.Mailer
{
	public class BizometMailer : MailerContextAbstract, IBizometMailer
	{
		public BizometMailer()
		{
			this.DefaultRecipients.Add(new EmailAddressModel("admin", "admin@bizomet.com"));
		}

		public IMailerContextResult ConfirmRegistrationMail(ConfirmRegistrationModel emailModel)
		{
			return this.HtmlMail(new EmailAddressModel(emailModel.UserName, emailModel.Email), $"Bizomet account email confirmation", emailModel);
		}

		public IMailerContextResult AccountIsLockedOutMail(ResetPasswordEmailModel emailModel)
		{
			return this.HtmlMail(new EmailAddressModel(emailModel.UserName, emailModel.Email), $"Bizomet account is locked out", emailModel);
		}

		public IMailerContextResult ResetPasswordMail(ResetPasswordEmailModel emailModel)
		{
			return this.HtmlMail(new EmailAddressModel(emailModel.UserName, emailModel.Email), $"Bizomet account reset password request", emailModel);
		}

		public IMailerContextResult WelcomeMail(string username, string email)
		{
			return this.HtmlMail(new EmailAddressModel(username, email), $"Welcome {username}!", new WelcomeModel() { UserName = username, Date = DateTime.Now });
		}
	}
}
