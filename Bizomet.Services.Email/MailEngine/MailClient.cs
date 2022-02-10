using System.Linq.Expressions;
using HtmlAgilityPack;
using MailKitMailer.Interfaces;
using MailKitMailer.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Utils;

namespace MailKitMailer.MailEngine
{
	/// <summary>
	/// Mail Client used for sending the prepared mail messages from MailerContexts
	/// </summary>
	/// <seealso cref="MailKitMailer.Interfaces.IMailClient" />
	public class MailClient : IMailClient
	{
		/// <summary>
		/// The SMTP configuration
		/// </summary>
		private readonly IOptions<SMTPConfigModel> smtpConfig;
		/// <summary>
		/// The client
		/// </summary>
		private readonly IMailKitSMTPClient client;
		/// <summary>
		/// The service provider
		/// </summary>
		private readonly IServiceProvider serviceProvider;
		/// <summary>
		/// The razor view engine
		/// </summary>
		private readonly IMailerViewEngine razorViewEngine;
		/// <summary>
		/// The temporary data provider
		/// </summary>
		private readonly ITempDataProvider tempDataProvider;
		/// <summary>
		/// The HTTP Context accessor
		/// </summary>
		private readonly IHttpContextAccessor httpContextAccessor;

		private readonly HttpClient httpClient;

		private readonly IWebHostEnvironment hostingEnvironment;

		private readonly ILogger<MailClient> _logger;

		/// <summary>
		/// Initializes a new instance of the <see cref="MailClient" /> class.
		/// </summary>
		/// <param name="smtpConfig">The SMTP configuration.</param>
		/// <param name="client">The client.</param>
		/// <param name="serviceProvider">The service provider.</param>
		/// <param name="razorViewEngine">The razor view engine.</param>
		/// <param name="tempDataProvider">The temporary data provider.</param>
		/// <param name="httpContextAccessor">The HTTP Context accessor.</param>
		public MailClient(
			IOptions<Models.SMTPConfigModel> smtpConfig,
			IMailKitSMTPClient client,
			IServiceProvider serviceProvider,
			IMailerViewEngine razorViewEngine,
			ITempDataProvider tempDataProvider,
			IEnumerable<IHttpContextAccessor> httpContextAccessor,
			IHttpClientFactory httpClientFactory,
			IWebHostEnvironment hostingEnvironment,
			ILogger<MailClient> logger)
		{
			this.smtpConfig = smtpConfig;
			this.client = client;
			this.serviceProvider = serviceProvider;
			this.razorViewEngine = razorViewEngine;
			this.tempDataProvider = tempDataProvider;
			this.httpContextAccessor = httpContextAccessor?.Count() > 0 ? httpContextAccessor.FirstOrDefault() : null;
			this.httpClient = httpClientFactory.CreateClient("attachmentDownloader");
			this.hostingEnvironment = hostingEnvironment;
			this._logger = logger;
		}

		/// <summary>
		/// Sends the message prepared by given mailer <typeparam name="TContext"> Mailer Context
		/// </summary>
		/// <typeparam name="TContext">The type of the mailer Context.</typeparam>
		/// <param name="ContextBuilder">The Context builder.</param>
		public void Send<TContext>(Expression<Func<TContext, IMailerContextResult>> ContextBuilder) where TContext : class, IMailerContext
		{
			TContext ctx = this.serviceProvider.GetService(typeof(TContext)) as TContext;

			if (ctx == null) {
				throw new Exception($"Mailer Context of type {typeof(TContext)} were not found.");
			}

			ctx.OnBeforeSend(this.serviceProvider);

			IMailerContextResult result = this._CompileMailerContext<TContext>(ContextBuilder);
			MimeMessage message = this.PrepareMessage(this.serviceProvider.GetService(typeof(TContext)) as TContext, result).Result;

			this.client.CheckCertificateRevocation = this.smtpConfig.Value?.CheckCertificateRevocation ?? true;
			this.client.Connect(this.smtpConfig.Value?.Host, this.smtpConfig.Value?.Port ?? 0, this.smtpConfig.Value?.UseSSL ?? false);

			if (this.smtpConfig.Value.DoAuthenticate) {
				this.client.Authenticate(this.smtpConfig.Value.Username, this.smtpConfig.Value.Password);
			}

			this.client.Send(message);

			_logger.LogDebug($"Email sent to {message.To.ToString()}");

			ctx.OnAfterSend(this.serviceProvider);
		}

		/// <summary>
		/// Sends the message prepared by given mailer <typeparam name="TContext"> Mailer Context asynchronous.
		/// </summary>
		/// <typeparam name="TContext">The type of the Context.</typeparam>
		/// <param name="ContextBuilder">The Context builder.</param>
		/// <returns></returns>
		public async Task SendAsync<TContext>(Expression<Func<TContext, IMailerContextResult>> ContextBuilder) where TContext : class, IMailerContext
		{
			TContext ctx = this.serviceProvider.GetService(typeof(TContext)) as TContext;
			if (ctx == null) {
				throw new Exception($"Mailer Context of type {typeof(TContext)} were not found.");
			}

			ctx.OnBeforeSend(this.serviceProvider);

			IMailerContextResult result = this._CompileMailerContext<TContext>(ContextBuilder);
			MimeMessage message = await this.PrepareMessage(this.serviceProvider.GetService(typeof(TContext)) as TContext, result);

			this.client.CheckCertificateRevocation = this.smtpConfig.Value?.CheckCertificateRevocation ?? true;

			await this.client.ConnectAsync(this.smtpConfig.Value?.Host, this.smtpConfig.Value.Port, this.smtpConfig.Value.UseSSL);

			if (this.smtpConfig.Value.DoAuthenticate) {
				await this.client.AuthenticateAsync(this.smtpConfig.Value.Username, this.smtpConfig.Value.Password);
			}

			await this.client.SendAsync(message);

			_logger.LogDebug($"Email sent to {message.To.ToString()}");

			ctx.OnAfterSend(this.serviceProvider);
		}

		/// <summary>
		/// Prepares the <see cref="MimeMessage"/> message.
		/// </summary>
		/// <param name="Context">The Context.</param>
		/// <param name="result">The result.</param>
		/// <returns></returns>
		private async Task<MimeMessage> PrepareMessage(IMailerContext Context, IMailerContextResult result)
		{
			MimeMessage message = new MimeKit.MimeMessage();
			message.Subject = result.Subject;

			EmailAddressModel from = result.From ?? Context.From ?? this.smtpConfig.Value?.FromAddress ?? new EmailAddressModel() { Email = "root@localhost", Name = "Root" };

			List<EmailAddressModel> to = new List<EmailAddressModel>(result.To);
			List<EmailAddressModel> cc = new List<EmailAddressModel>(result.CC);
			List<EmailAddressModel> bcc = new List<EmailAddressModel>(result.BCC);

			if (Context.DefaultRecipients != null) {
				to.AddRange(Context.DefaultRecipients);
			}

			if (Context.DefaultCCRecipients != null) {
				cc.AddRange(Context.DefaultCCRecipients);
			}

			if (Context.DefaultBCCRecipients != null) {
				bcc.AddRange(Context.DefaultBCCRecipients);
			}

			//From
			message.From.Add(new MailboxAddress(from.Name, from.Email));

			// To
			message.To.AddRange(to.Select(x => new MailboxAddress(x.Name, x.Email)));

			// CC
			message.Cc.AddRange(cc.Select(x => new MailboxAddress(x.Name, x.Email)));

			// Add defaults for cc
			message.Bcc.AddRange(bcc.Select(x => new MailboxAddress(x.Name, x.Email)));

			Context.DefaultBCCRecipients?.ToList().ForEach(addr => message.Bcc.Add(new MailboxAddress(addr.Name, addr.Email)));

			// Render the view
			string htmlBody = await this._RenderView(result, Context);
			string textBody = result.TextBody ?? "";

			if (string.IsNullOrEmpty(textBody)) {
				textBody = _HtmlToPlainText(htmlBody);
			}

			var bodyBuilder = new BodyBuilder();
			bodyBuilder.HtmlBody = htmlBody;
			bodyBuilder.TextBody = textBody;

			var htmlDoc = new HtmlDocument();
			htmlDoc.LoadHtml(htmlBody);
			var imgNodes = htmlDoc.DocumentNode.SelectNodes("//img[@src]");
			if (imgNodes != null) {
				foreach (HtmlNode node in imgNodes) {
					var src = node.GetAttributeValue("src", string.Empty);
					if (src.StartsWith("cid:")) {

						var imageFileName = hostingEnvironment.WebRootPath + $"\\img\\{src.Split(':')[1]}";
						//if (File.Exists(imageFileName)) {
							byte[] imageArray = File.ReadAllBytes(imageFileName);

							var image = bodyBuilder.LinkedResources.Add(src.Replace("cid:", string.Empty), imageArray);
							image.ContentId = MimeUtils.GenerateMessageId();
							node.SetAttributeValue("src", string.Format("cid:{0}", image.ContentId));
						//}
					}
				}
			}

			bodyBuilder.HtmlBody = htmlDoc.DocumentNode.WriteTo();

			// Attachments
			if (result.Attachments != null && !result.Attachments.IsEmpty()) {
				foreach (var attachment in result.Attachments) {
					if (attachment.FilePath != null) {
						MimePart att = null;

						if (attachment.ContentType != null) {
							att = new MimePart(attachment.ContentType);
						}
						else {
							att = new MimePart(MimeKit.MimeTypes.GetMimeType(attachment.FilePath));
						}

						att.Content = new MimeContent(File.OpenRead(attachment.FilePath), ContentEncoding.Default);
						att.ContentDisposition = new ContentDisposition(ContentDisposition.Attachment);
						att.ContentTransferEncoding = ContentEncoding.Base64;
						att.FileName = Path.GetFileName(attachment.FilePath);

						bodyBuilder.Attachments.Add(att);
					}
					else if (attachment.FileUrl != null) {

						var data = await this.httpClient.GetByteArrayAsync(attachment.FileUrl);
						var content = new System.IO.MemoryStream(data);
						string fname = Path.GetFileName(attachment.FileUrl.ToString());

						if (!Path.HasExtension(fname)) {
							// We got no valid file name. Lets see if we find an content disposition.
							var rr = await this.httpClient.GetAsync(attachment.FileUrl);
							var headers = rr.Content.Headers;

							if (headers != null && headers.ContentDisposition != null) {
								string cdname = headers.ContentDisposition.FileName;

								if (!string.IsNullOrEmpty(cdname)) {
									fname = cdname;
								}
							}

						}

						MimePart att = null;

						if (attachment.ContentType != null) {
							att = new MimePart(attachment.ContentType);
						}
						else {
							att = new MimePart(MimeKit.MimeTypes.GetMimeType(fname));
						}

						att.Content = new MimeContent(content, ContentEncoding.Default);
						att.ContentDisposition = new ContentDisposition(ContentDisposition.Attachment);
						att.ContentTransferEncoding = ContentEncoding.Base64;
						att.FileName = fname;

						bodyBuilder.Attachments.Add(att);
					}
				}
			}

			message.Body = bodyBuilder.ToMessageBody();

			return message;
		}

		/// <summary>
		/// Converts  HTML content to plain text for the additional text body.
		/// </summary>
		/// <param name="html">The HTML.</param>
		/// <returns></returns>
		private string _HtmlToPlainText(string html)
		{
			HtmlDocument doc = new HtmlDocument();
			doc.LoadHtml(html);
			StringWriter writer = new StringWriter();

			this._ExtractText(doc.DocumentNode, writer);

			return writer.ToString();
		}

		/// <summary>
		/// Extracts the text of the given html node.
		/// </summary>
		/// <param name="node">The node.</param>
		/// <param name="writer">The writer.</param>
		private void _ExtractText(HtmlNode node, StringWriter writer)
		{
			switch (node.NodeType) {
				case HtmlNodeType.Comment:
					return;

				case HtmlNodeType.Text:

					string parentName = node.ParentNode.Name;
					if ((parentName == "script") || (parentName == "style"))
						break;

					string html = ((HtmlTextNode) node).Text;

					if (HtmlNode.IsOverlappedClosingElement(html))
						break;

					// check the text is meaningful and not a bunch of whitespaces
					if (html.Trim().Length > 0) {
						writer.Write(HtmlEntity.DeEntitize(html));
					}

					break;

				case HtmlNodeType.Element:
					switch (node.Name) {
						case "p":
							writer.Write("\r\n\n");
							break;
						case "br":
							writer.Write("\r\n");
							break;
					}

					break;
			}

			if (node.HasChildNodes) {
				node.ChildNodes.ToList()
					.ForEach(n => this._ExtractText(n, writer));
			}
		}

		/// <summary>
		/// Renders the view.
		/// </summary>
		/// <param name="result">The result.</param>
		/// <param name="mailerContext">The mailer Context.</param>
		/// <returns></returns>
		/// <exception cref="System.ArgumentNullException"></exception>
		private async Task<string> _RenderView(IMailerContextResult result, IMailerContext mailerContext)
		{
			var httpContext = this.httpContextAccessor?.HttpContext ?? new DefaultHttpContext() { RequestServices = this.serviceProvider };
			var actionContext = new ActionContext(httpContext, new RouteData(), new ActionDescriptor());

			// When text, we dont need any view rendering if we got a text body
			if (!result.IsHtml && !string.IsNullOrEmpty(result.TextBody) && result.View == null) {
				return result.TextBody;
			}

			using (var sw = new StringWriter()) {
				string ContextName = mailerContext.GetType().Name;

				var viewResult = this.razorViewEngine.FindView(actionContext, $"{ContextName}/{result.View}", true);

				if (viewResult?.View == null) {
					viewResult = this.razorViewEngine.FindView(actionContext, $"{result.View}", true);
				}


				if (viewResult == null) {
					throw new Exception($"View not found by names {result.View}");
				}

				var viewDictionary = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary()) {
					Model = result.Model

				};

				var viewContext = new ViewContext(
					actionContext,
					viewResult.View,
					viewDictionary,
					new TempDataDictionary(actionContext.HttpContext, this.tempDataProvider),
					sw,
					new HtmlHelperOptions()
				);

				await viewResult.View.RenderAsync(viewContext);

				return sw.ToString();
			}
		}

		/// <summary>
		/// Compiles the mailer Context expression.
		/// </summary>
		/// <typeparam name="TContext">The type of the Context.</typeparam>
		/// <param name="expression">The expression.</param>
		/// <returns></returns>
		/// <exception cref="System.Exception">
		/// Invalid MailerContext Expression. Expecting call to Method and not static values. E.g. ::Send<IMyMailerContext>(x => x.HelloMail()).
		/// or
		/// Unable to load MailerContext {typeof(TContext).FullName}. Service Provider returned null.
		/// </exception>
		private IMailerContextResult _CompileMailerContext<TContext>(Expression<Func<TContext, IMailerContextResult>> expression) where TContext : class, IMailerContext
		{
			MethodCallExpression exp = expression.Body as MethodCallExpression;

			if (exp == null) {
				throw new Exception("Invalid MailerContext Expression. Expecting call to Method and not static values. E.g. ::Send<IMyMailerContext>(x => x.HelloMail()).");
			}

			string defaultViewName = exp.Method.Name;

			// Load Mailer Context
			TContext mailerContext = this.serviceProvider.GetService(typeof(TContext)) as TContext;

			if (mailerContext == null) {
				throw new Exception($"Unable to load MailerContext {typeof(TContext).FullName}. Service Provider returned null.");
			}

			// Compile expression
			IMailerContextResult r = expression.Compile()(mailerContext);

			if (string.IsNullOrEmpty(r.View) && (string.IsNullOrWhiteSpace(r.TextBody) || r.IsHtml)) {
				r.View = defaultViewName;
			}

			return r;
		}
	}
}
