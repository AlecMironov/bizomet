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
		private IRepositoryBase<Inquiry> _inquiryRepository;
		private IRepositoryBase<Project> _projectRepository;
		private IRepositoryBase<ProjectAttachment> _projectAttachments;
		private IRepositoryBase<RefreshToken> _refreshTokenRepository;

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

		public IRepositoryBase<Inquiry> Inquiries {
			get {
				if (_inquiryRepository == null)
					_inquiryRepository = new RepositoryBase<Inquiry>(_repositoryContext);

				return _inquiryRepository;
			}
		}

		public IRepositoryBase<Project> Projects {
			get {
				if (_projectRepository == null)
					_projectRepository = new RepositoryBase<Project>(_repositoryContext);

				return _projectRepository;
			}
		}

		public IRepositoryBase<ProjectAttachment> ProjectAttachments {
			get {
				if (_projectAttachments == null)
					_projectAttachments = new RepositoryBase<ProjectAttachment>(_repositoryContext);

				return _projectAttachments;
			}
		}

		public IRepositoryBase<RefreshToken> RefreshTokens {
			get {
				if (_refreshTokenRepository == null)
					_refreshTokenRepository = new RepositoryBase<RefreshToken>(_repositoryContext);

				return _refreshTokenRepository;
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
