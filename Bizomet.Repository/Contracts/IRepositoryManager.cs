namespace Bizomet.Contracts
{
	public interface IRepositoryManager
	{
		IProfileRepository UserProfile { get; }

		void Save();
	}
}
