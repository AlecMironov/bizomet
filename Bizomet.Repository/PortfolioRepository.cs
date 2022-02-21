using Bizomet.Contracts;
using Bizomet.Data;
using Bizomet.Data.Entities;

namespace Bizomet.Repository
{
	public class PortfolioRepository : RepositoryBase<UserPortfolio>, IPortfolioRepository
	{
		public PortfolioRepository(ApplicationDbContext repositoryContext)
			: base(repositoryContext)
		{
		}
	}
}
