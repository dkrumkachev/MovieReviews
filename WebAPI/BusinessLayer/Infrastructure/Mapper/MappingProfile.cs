using AutoMapper;
using BusinessLayer.Constants;
using BusinessLayer.Models.Country;
using BusinessLayer.Models.Director;
using BusinessLayer.Models.Genre;
using BusinessLayer.Models.Movie;
using BusinessLayer.Models.Review;
using BusinessLayer.Models.User;
using DataLayer.Models;

namespace BusinessLayer.Infrastructure.Mapper
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Country, CountryDTO>();
			CreateMap<CountryCreateDTO, Country>();
			CreateMap<CountryUpdateDTO, Country>();

			CreateMap<Director, DirectorDTO>();
			CreateMap<DirectorCreateDTO, Director>();
			CreateMap<DirectorUpdateDTO, Director>();

			CreateMap<Genre, GenreDTO>();
			CreateMap<GenreCreateDTO, Genre>();
			CreateMap<GenreUpdateDTO, Genre>();

			CreateMap<Movie, MovieDTO>();
			CreateMap<MovieCreateDTO, Movie>();
			CreateMap<MovieUpdateDTO, Movie>();

			CreateMap<Review, ReviewDTO>();
			CreateMap<ReviewCreateDTO, Review>();
			CreateMap<ReviewUpdateDTO, Review>();

			CreateMap<User, UserDTO>();
			CreateMap<UserUpdateUsernameDTO, User>();
			CreateMap<UserUpdatePasswordDTO, User>();
			CreateMap<User, AuthenticatedUserDTO>()
				.ForMember(dto => dto.Role, opt => opt
					.MapFrom(user => user.IsAdmin ? RoleNames.Admin : RoleNames.User));
			CreateMap<UserRegisterDTO, User>();
		}
	}
}
