using System.Reflection;
using System.Text;
using System.Linq.Dynamic.Core;
using DataLayer.Models;

namespace DataLayer.Repositories.Implementations
{
	public static class MovieRepositoryExtensions
	{
		public static IQueryable<Movie> Search(this IQueryable<Movie> movies, string searchTerm)
		{
			if (string.IsNullOrWhiteSpace(searchTerm))
			{
				return movies;
			}
			var lowerCaseTerm = searchTerm.Trim().ToLower();
			return movies.Where(m => m.Title.ToLower().Contains(lowerCaseTerm));
		}

		public static IQueryable<Movie> Sort(this IQueryable<Movie> movies, string orderByQueryString)
		{
			if (string.IsNullOrWhiteSpace(orderByQueryString))
			{
				return movies;
			}
			var orderParams = orderByQueryString.Trim().Split(',');
			var propertyInfos = typeof(Movie).GetProperties(BindingFlags.Public | BindingFlags.Instance);
			var orderQueryBuilder = new StringBuilder();
			foreach (var param in orderParams)
			{
				if (string.IsNullOrWhiteSpace(param))
				{
					continue;
				}
				var propertyFromQueryName = param.Split(" ")[0];
				var objectProperty = propertyInfos.FirstOrDefault(property =>
					property.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));
				if (objectProperty != null)
				{
					var direction = param.EndsWith(" desc") ? "descending" : "ascending";
					orderQueryBuilder.Append($"{objectProperty.Name.ToString()} {direction}, ");
				}
			}
			var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');
			if (string.IsNullOrWhiteSpace(orderQuery))
			{
				return movies;
			}
			return movies.OrderBy(orderQuery);
		}
	}
}
