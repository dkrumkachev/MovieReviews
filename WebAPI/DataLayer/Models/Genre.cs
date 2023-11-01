namespace DataLayer.Models
{
	public class Genre : BaseModel
	{
		public string Name { get; set; }

		public ICollection<Movie> Movies { get; set; }
	}
}
