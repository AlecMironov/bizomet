using System.Linq.Expressions;
using Bizomet.Data.Configurations;
using Bizomet.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bizomet.Data
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string,
		IdentityUserClaim<string>,
		ApplicationUserRole,
		IdentityUserLogin<string>,
		IdentityRoleClaim<string>,
		IdentityUserToken<string>>
	{
		public DbSet<UserProfile> UserProfile { get; set; }

		public ApplicationDbContext(DbContextOptions options)
			: base(options)
		{
		}

		/// <summary>
		/// Get entity of given type by primary key.
		/// </summary>
		public TEntity GetByKey<TEntity>(object keyValue) where TEntity : EntityBase
		{
			var result = Set<TEntity>().Find(keyValue);
			if (result == null) 
				throw new KeyNotFoundException("Entity not found");

			return result;
		}

		public void Delete<TEntity>(object keyValue) where TEntity : EntityBase
		{
			var result = GetByKey<TEntity>(keyValue);
			Set<TEntity>().Remove(result);
			SaveChanges();
		}

		/// <summary>
		/// Get query containing entities of given type, including given entity navigation properties if specified.
		/// </summary>
		/// <typeparam name="TEntity">Type of entity.</typeparam>
		/// <param name="includeProperties">Expression representing entity navigation properties to include in query.</param>
		/// <returns>IQueryable representing query containing entities of given type.</returns>
		public IQueryable<TEntity> GetQuery<TEntity>(params Expression<Func<TEntity, object>>[] includeProperties) where TEntity : EntityBase
		{
			var result = Set<TEntity>().AsNoTracking<TEntity>();

			foreach (Expression<Func<TEntity, object>> includeProperty in includeProperties)
				result = result.Include<TEntity, object>(includeProperty);

			return result;
		}

		///// <summary>
		///// Saves all changes made in this context to the underlying database.
		///// </summary>
		///// <returns>
		///// The number of state entries written to the underlying database. This can include
		///// state entries for entities and/or relationships. Relationship state entries are
		///// created for many-to-many relationships and relationships where there is no foreign
		///// key property included in the entity class (often referred to as independent associations).
		/////</returns>
		public override int SaveChanges()
		{
			BeforeSaving();
			return base.SaveChanges();
		}

		///// <summary>
		///// Asynchronously saves all changes made in this context to the underlying database.
		///// </summary>
		///// <returns>
		///// A task that represents the asynchronous save operation. The task result contains
		///// the number of state entries written to the underlying database. This can include
		///// state entries for entities and/or relationships. Relationship state entries are
		///// created for many-to-many relationships and relationships where there is no foreign
		///// key property included in the entity class (often referred to as independent associations).
		/////</returns>
		public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
		{
			BeforeSaving();
			return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
		}

		private void BeforeSaving()
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.ApplyConfiguration(new ApplicationUserConfiguration());
			modelBuilder.ApplyConfiguration(new ApplicationRoleConfiguration());
			modelBuilder.ApplyConfiguration(new UserProfileConfiguration());
		}
	}
}