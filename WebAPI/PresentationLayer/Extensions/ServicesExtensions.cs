using BusinessLayer.Infrastructure.Mapper;
using BusinessLayer.Infrastructure.Validators.Movie;
using BusinessLayer.Services.Contracts;
using BusinessLayer.Services.Implementations;
using DataLayer.Data;
using DataLayer.Repositories.UnitOfWork;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PresentationLayer.ActionFilters;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using System.Reflection;
using System.Text;

namespace PresentationLayer.Extensions
{
	public static class ServicesExtensions
	{
		public static void ConfigureActionFilters(this IServiceCollection services)
		{
			services.AddScoped<ValidationFilterAttribute>();
			services.AddScoped<ReviewAccessFilterAttribute>();
		}

		public static void ConfigureBusinessLayerServices(this IServiceCollection services)
		{
			services.AddSingleton<IConfigurationService, ConfigurationService>();
			services.AddScoped<IAuthenticationService, AuthenticationService>();
			services.AddScoped<ITokenService, JwtTokenService>();
			services.AddScoped<IPasswordService, PasswordService>();
			services.AddScoped<ICountryService, CountryService>();
			services.AddScoped<IDirectorService, DirectorService>();
			services.AddScoped<IGenreService, GenreService>();
			services.AddScoped<IMovieService, MovieService>();
			services.AddScoped<IReviewService, ReviewService>();
			services.AddScoped<IUserService, UserService>();
		}

		public static void ConfigureRepositoryManager(this IServiceCollection services)
		{
			services.AddScoped<IRepositoryManager, RepositoryManager>();
		}

		public static void ConfigureSqlServerDbContext(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<DataContext>(opt =>
				opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
		}

		public static void ConfigureAutoMapper(this IServiceCollection services)
		{
			services.AddAutoMapper(typeof(MappingProfile));
		}

		public static void ConfigureFluentValidation(this IServiceCollection services)
		{
			services.AddValidatorsFromAssemblyContaining<MovieCreateDTOValidator>();
			services.AddFluentValidationAutoValidation();
		}

		public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
		{
			IConfigurationService configurationService = new ConfigurationService(configuration);
			var issuer = configurationService.GetSetting("JwtSettings:ValidIssuer");
			var audience = configurationService.GetSetting("JwtSettings:ValidAudience");
			var key = configurationService.GetSetting("JwtSettings:Key");

			services.AddAuthentication(opt =>
			{
				opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(opt =>
			{
				opt.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateIssuerSigningKey = true,
					ValidIssuer = issuer,
					ValidAudience = audience,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
				};
			});
		}

		public static void ConfigureSwagger(this IServiceCollection services)
		{
			services.AddSwaggerGen(options =>
			{
				options.SwaggerDoc("v1", new OpenApiInfo
				{
					Title = "MovieReviews API",
					Version = "v1",
				});
				options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					Name = "Authorization",
					Description = "Place to add JWT with Bearer.",
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.Http,
					Scheme = "Bearer"
				});
				options.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type = ReferenceType.SecurityScheme,
								Id = "Bearer"
							}
						},
						Array.Empty<string>()
					}
				});
				var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
				var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
				options.IncludeXmlComments(xmlPath);
			});
		}
	}
}
