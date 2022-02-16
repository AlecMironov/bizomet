using System.Linq.Expressions;
using Bizomet.Contracts;
using Bizomet.Data;
using Bizomet.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bizomet.Repository
{
	public class RepositoryBase<T> : IRepositoryBase<T>
		where T : EntityBase
	{
		protected ApplicationDbContext RepositoryContext;

		public RepositoryBase(ApplicationDbContext repositoryContext)
		{
			RepositoryContext = repositoryContext;
		}

		public virtual T Get(object id) => RepositoryContext.GetByKey<T>(id);

		public virtual IQueryable<T> GetAll() => RepositoryContext.GetQuery<T>();

		public virtual IQueryable<T> GetAll(Expression<Func<T, bool>> expression) =>
			RepositoryContext.GetQuery<T>().Where(expression);

		public virtual void Create(T entity) => RepositoryContext.Set<T>().Add(entity);

		public virtual void Update(T entity) => RepositoryContext.Set<T>().Update(entity);

		public virtual void Delete(T entity) => RepositoryContext.Set<T>().Remove(entity);
	}
}
