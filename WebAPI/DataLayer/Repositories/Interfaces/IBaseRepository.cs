using DataLayer.Models;

namespace DataLayer.Repositories.Interfaces
{
	public interface IBaseRepository<T> where T : BaseModel
	{
		IEnumerable<T> GetAll();

		T? GetById(int id);

		bool IdExists(int id);

		void Create(T entity);

		void Update(T entity);

		void Delete(T entity);

		void Save();
	}
}
