using Bizomet.Data.Models;

namespace Bizomet.Contracts
{
	public interface ICompanyRepository
	{
		IEnumerable<Company> GetAllCompanies();
	}
}
