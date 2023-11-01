using DataLayer.Data;
using DataLayer.Models;
using DataLayer.Repositories.Contracts;

namespace DataLayer.Repositories.Implementations
{
	public class DirectorRepository : BaseRepository<Director>, IDirectorRepository
	{
		public DirectorRepository(DataContext context) : base(context) { }
	}
}
