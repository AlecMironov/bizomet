using Bizomet.Data.Entities;

namespace Bizomet.Contracts
{
	public interface IRepositoryManager
	{
		IProfileRepository UserProfile { get; }

		IPortfolioRepository UserPortfolio { get; }

		IRepositoryBase<ContactUsRequest> ContactUsRequestRepository { get; }

		void Save();
	}
}
