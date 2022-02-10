using Bizomet.Models.MailModels;
using MailKitMailer.Interfaces;

namespace Bizomet.Mailer
{
	public interface IBizometMailer : IMailerContext
	{
		IMailerContextResult WelcomeMail(string username, string email);
		IMailerContextResult ConfirmRegistrationMail(ConfirmRegistrationModel emailModel);
		IMailerContextResult AccountIsLockedOutMail(ResetPasswordEmailModel emailModel);
		IMailerContextResult ResetPasswordMail(ResetPasswordEmailModel emailModel);
	}
}