using System.Linq.Expressions;
using Bizomet.Contracts;
using Bizomet.Data;
using Microsoft.EntityFrameworkCore;

namespace Bizomet.Repository
{
	public class RepositoryBase<T> : IRepositoryBase<T>
		where T : class
	{
		protected ApplicationDbContext RepositoryContext;

		public RepositoryBase(ApplicationDbContext repositoryContext)
		{
			RepositoryContext = repositoryContext;
		}

		public IQueryable<T> GetAll() => RepositoryContext.Set<T>().AsNoTracking();

		public IQueryable<T> GetAll(Expression<Func<T, bool>> expression) =>
			RepositoryContext.Set<T>().Where(expression).AsNoTracking();

		public void Create(T entity) => RepositoryContext.Set<T>().Add(entity);

		public void Update(T entity) => RepositoryContext.Set<T>().Update(entity);

		public void Delete(T entity) => RepositoryContext.Set<T>().Remove(entity);
	}
}
