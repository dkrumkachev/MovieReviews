namespace DataLayer.Models
{
	public class Director : BaseModel
	{
		public string Name { get; set; }

		public ICollection<Movie> Movies { get; set; }
	}
}
