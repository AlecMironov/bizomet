using Bizomet.Contracts;
using Bizomet.Data;
using Bizomet.Data.Entities;

namespace Bizomet.Repository
{
	public class RepositoryManager : IRepositoryManager
	{
		private ApplicationDbContext _repositoryContext;
		private IProfileRepository _profileRepository;
		private IPortfolioRepository _portfolioRepository;
		private IRepositoryBase<ContactUsRequest> _contactUsRequestRepository;

		public RepositoryManager(ApplicationDbContext repositoryContext)
		{
			_repositoryContext = repositoryContext;
		}

		public IProfileRepository UserProfile {
			get {
				if (_profileRepository == null)
					_profileRepository = new ProfileRepository(_repositoryContext);

				return _profileRepository;
			}
		}

		public IPortfolioRepository UserPortfolio {
			get {
				if (_portfolioRepository == null)
					_portfolioRepository = new PortfolioRepository(_repositoryContext);

				return _portfolioRepository;
			}
		}

		public IRepositoryBase<ContactUsRequest> ContactUsRequestRepository {
			get {
				if (_contactUsRequestRepository == null)
					_contactUsRequestRepository = new RepositoryBase<ContactUsRequest>(_repositoryContext);

				return _contactUsRequestRepository;
			}
		}

		public void Save() => _repositoryContext.SaveChanges();
	}
}
