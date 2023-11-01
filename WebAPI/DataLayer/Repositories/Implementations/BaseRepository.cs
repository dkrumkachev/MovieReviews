using DataLayer.Data;
using DataLayer.Models;
using DataLayer.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories.Implementations
{
	public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseModel
	{
		protected readonly DataContext context;
		protected readonly DbSet<T> dbSet;

		protected BaseRepository(DataContext context)
		{
			this.context = context;
			dbSet = context.Set<T>();
		}

		public virtual T? GetById(int id) 
			=> dbSet.AsNoTracking().FirstOrDefault(x => x.Id == id);

		public virtual IEnumerable<T> GetAll() 
			=> dbSet.AsNoTracking().ToList();

		public virtual bool IdExists(int id)
			=> dbSet.Any(x => x.Id == id);

		public virtual void Create(T entity) 
			=> context.Add(entity);

		public virtual void Delete(T entity) 
			=> context.Remove(entity);

		public virtual void Update(T entity) 
			=> context.Update(entity);

		public virtual void Save() 
			=> context.SaveChanges();
	}
}
