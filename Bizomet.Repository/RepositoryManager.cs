using Bizomet.Contracts;
using Bizomet.Data;

namespace Bizomet.Repository
{
	public class RepositoryManager : IRepositoryManager
	{
		private ApplicationDbContext _repositoryContext;
		private IProfileRepository _profileRepository;
		private IPortfolioRepository _portfolioRepository;

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

		public void Save() => _repositoryContext.SaveChanges();
	}
}
