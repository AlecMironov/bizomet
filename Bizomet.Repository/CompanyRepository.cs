//using Bizomet.Contracts;
//using Bizomet.Data;
//using Bizomet.Data.Models;

//namespace Bizomet.Repository
//{
//	public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
//	{
//		public CompanyRepository(ApplicationDbContext repositoryContext)
//			: base(repositoryContext)
//		{
//		}

//		public IEnumerable<Company> GetAllCompanies() => GetAll().OrderBy(c => c.Name).ToList();
//	}
//}
