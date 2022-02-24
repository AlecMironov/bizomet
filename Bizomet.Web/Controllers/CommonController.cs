using AutoMapper;
using Bizomet.Contracts;
using Bizomet.Core.Enums;
using Bizomet.Data.Entities;
using Bizomet.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bizomet.Web.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class CommonController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IRepositoryManager _repositoryManager;
		private readonly IMapper _mapper;
		private readonly ILogger<CommonController> _logger;

		public CommonController(UserManager<ApplicationUser> userManager, IMapper mapper, IRepositoryManager repositoryManager, ILogger<CommonController> logger)
		{
			_userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
			_repositoryManager = repositoryManager ?? throw new ArgumentNullException(nameof(repositoryManager));
			_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		[HttpGet]
		[Produces("application/json")]
		public IActionResult ContactUsReason()
		{
			var result = new List<KeyValuePairModel>() {
				new KeyValuePairModel() { code = ContactReason.GENERAL.ToString(), name = Core.Helpers.EnumHelper.GetEnumName(ContactReason.GENERAL) },
				new KeyValuePairModel() { code = ContactReason.TECHNICAL.ToString(), name = Core.Helpers.EnumHelper.GetEnumName(ContactReason.TECHNICAL) },
				new KeyValuePairModel() { code = ContactReason.SHARE_SUCCESS.ToString(), name = Core.Helpers.EnumHelper.GetEnumName(ContactReason.SHARE_SUCCESS) },
				new KeyValuePairModel() { code = ContactReason.FEEDBACK.ToString(), name = Core.Helpers.EnumHelper.GetEnumName(ContactReason.FEEDBACK) },
				new KeyValuePairModel() { code = ContactReason.REPORT_SPAM.ToString(), name = Core.Helpers.EnumHelper.GetEnumName(ContactReason.REPORT_SPAM) },
			};
			return Json(result);
		}
	}
}
