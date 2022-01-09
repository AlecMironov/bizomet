using Bizomet.Contracts;
using Bizomet.Data;

namespace Bizomet.Repository
{
	public class RepositoryManager : IRepositoryManager
	{
		private ApplicationDbContext _repositoryContext;
		private ICompanyRepository _companyRepository;

		public RepositoryManager(ApplicationDbContext repositoryContext)
		{
			_repositoryContext = repositoryContext;
		}

		public ICompanyRepository Company {
			get {
				if (_companyRepository == null)
					_companyRepository = new CompanyRepository(_repositoryContext);

				return _companyRepository;
			}
		}

		public void Save() => _repositoryContext.SaveChanges();
	}
}
