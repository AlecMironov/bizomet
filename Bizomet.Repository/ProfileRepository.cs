using Bizomet.Contracts;
using Bizomet.Data;
using Bizomet.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bizomet.Repository
{
	public class ProfileRepository : RepositoryBase<UserProfile>, IProfileRepository
	{
		public ProfileRepository(ApplicationDbContext repositoryContext)
			: base(repositoryContext)
		{
		}
	}
}
