using Bizomet.Data.Entities;

namespace Bizomet.Contracts
{
	public interface IRepositoryManager
	{
		IProfileRepository UserProfile { get; }

		IPortfolioRepository UserPortfolio { get; }

		IRepositoryBase<ContactUsRequest> ContactUsRequestRepository { get; }

		IRepositoryBase<Inquiry> Inquiries { get; }

		IRepositoryBase<Project> Projects { get; }

		void Save();
	}
}
