using System.Linq.Expressions;

namespace Bizomet.Contracts
{
	public interface IRepositoryBase<T>
	{
		T Get(object id);
		IQueryable<T> GetAll();
		IQueryable<T> GetAll(Expression<Func<T, bool>> expression);
		Task<bool> Exists(Expression<Func<T, bool>> expression);
		void Create(T entity);
		void Update(T entity);
		void Delete(T entity);
	}
}
