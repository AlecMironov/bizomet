using Bizomet.Contracts;
using Bizomet.Data;

namespace Bizomet.Repository
{
	public class RepositoryManager : IRepositoryManager
	{
		private ApplicationDbContext _repositoryContext;
		private IProfileRepository _profileRepository;

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

		public void Save() => _repositoryContext.SaveChanges();
	}
}
