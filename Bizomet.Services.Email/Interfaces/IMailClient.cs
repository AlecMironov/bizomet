using System.Linq.Expressions;

namespace MailKitMailer.Interfaces
{
	public interface IMailClient
	{
		/// <summary>
		/// Sends the message prepared by given mailer <typeparam name="TContext"> Mailer Context
		/// </summary>
		/// <typeparam name="TContext">The type of the mailer Context.</typeparam>
		/// <param name="ContextBuilder">The Context builder.</param>
		void Send<TContext>(Expression<Func<TContext, IMailerContextResult>> ContextBuilder) where TContext : class, IMailerContext;

		/// <summary>
		/// Sends the message prepared by given mailer <typeparam name="TContext"> Mailer Context asynchronous.
		/// </summary>
		/// <typeparam name="TContext">The type of the Context.</typeparam>
		/// <param name="ContextBuilder">The Context builder.</param>
		/// <returns></returns>
		Task SendAsync<TContext>(Expression<Func<TContext, IMailerContextResult>> ContextBuilder) where TContext : class, IMailerContext;
	}
}
