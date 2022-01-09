using AutoMapper;
using Bizomet.Contracts;
using Bizomet.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bizomet.Web.Controllers
{
	[Route("api/companies")]
	[Authorize]
	[ApiController]
	public class CompaniesController : ControllerBase
	{
		private readonly IRepositoryManager _repository;
		private readonly ILogger<CompaniesController> _logger;
		private readonly IMapper _mapper;

		public CompaniesController(IRepositoryManager repository, ILogger<CompaniesController> logger, IMapper mapper)
		{
			_repository = repository;
			_logger = logger;
			_mapper = mapper;
		}

		[HttpGet]
		public IActionResult GetCompanies()
		{
			try {
				var claims = User.Claims;

				var companies = _repository.Company.GetAllCompanies();
				var companiesModel = _mapper.Map<IEnumerable<CompanyViewModel>>(companies);

				return Ok(companiesModel);
			}
			catch (Exception ex) {
				_logger.LogError($"Something went wrong in the {nameof(GetCompanies)} action {ex}");
				return StatusCode(500, "Internal server error");
			}
		}
	}
}
