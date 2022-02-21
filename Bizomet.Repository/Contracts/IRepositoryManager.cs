namespace Bizomet.Contracts
{
	public interface IRepositoryManager
	{
		IProfileRepository UserProfile { get; }

		IPortfolioRepository UserPortfolio { get; }

		void Save();
	}
}
