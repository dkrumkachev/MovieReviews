using AutoMapper;
using BusinessLayer.Exceptions;
using BusinessLayer.Models.Review;
using BusinessLayer.Services.Interfaces;
using DataLayer.Models;
using DataLayer.Repositories.UnitOfWork;

namespace BusinessLayer.Services.Implementations
{
	public class ReviewService : IReviewService
	{

		private readonly IRepositoryManager repositoryManager;
		private readonly IMapper mapper;

		public ReviewService(IRepositoryManager repositoryManager, IMapper mapper)
		{
			this.repositoryManager = repositoryManager;
			this.mapper = mapper;
		}

		public ReviewDTO Create(ReviewCreateDTO review)
		{
			var movie = repositoryManager.Movies.GetById(review.MovieId)
				?? throw new EntityNotFoundException($"Movie with id {review.MovieId} does not exist.");
			if (repositoryManager.Users.IdExists(review.UserId))
			{
				throw new EntityNotFoundException($"User with id {review.UserId} does not exist.");
			}
			if (repositoryManager.Reviews.ReviewExists(review.UserId, review.MovieId))
			{
				throw new EntityAlreadyExistsException(
					$"User {review.UserId}'s review of movie {review.MovieId} already exists.");
			}
			var newReview = mapper.Map<Review>(review);
			repositoryManager.Reviews.Create(newReview);
			movie.ReviewsCount += 1;
			movie.Rating = CalculateMovieRating(movie, review.Rating);
			repositoryManager.Movies.Update(movie);
			repositoryManager.Save();
			return mapper.Map<ReviewDTO>(newReview);
		}

		public ReviewDTO Update(int reviewId, ReviewUpdateDTO review)
		{
			var reviewToUpdate = repositoryManager.Reviews.GetById(reviewId)
				?? throw new EntityNotFoundException($"Review with id {reviewId} does not exist.");
			if (review.Rating != reviewToUpdate.Rating)
			{
				var movie = repositoryManager.Movies.GetById(reviewToUpdate.MovieId)!;
				movie.Rating = CalculateMovieRating(movie, review.Rating - reviewToUpdate.Rating);
				repositoryManager.Movies.Update(movie);
			}
			mapper.Map(review, reviewToUpdate);
			repositoryManager.Reviews.Update(reviewToUpdate);
			repositoryManager.Save();
			return mapper.Map<ReviewDTO>(reviewToUpdate);
		}

		public void Delete(int reviewId)
		{
			var reviewToDelete = repositoryManager.Reviews.GetById(reviewId)
				?? throw new EntityNotFoundException($"Review with id {reviewId} does not exist.");
			var movie = repositoryManager.Movies.GetById(reviewToDelete.MovieId)!;
			repositoryManager.Reviews.Delete(reviewToDelete);
			movie.ReviewsCount -= 1;
			movie.Rating = CalculateMovieRating(movie, -reviewToDelete.Rating);
			repositoryManager.Movies.Update(movie);
			repositoryManager.Save();
		}

		public ReviewDTO GetById(int reviewId)
		{
			var review = repositoryManager.Reviews.GetById(reviewId)
				?? throw new EntityNotFoundException($"Review with id {reviewId} does not exist.");
			return mapper.Map<ReviewDTO>(review);
		}

		public IEnumerable<ReviewDTO> GetByUser(int userId)
		{
			var reviews = repositoryManager.Reviews.GetByUser(userId)
				?? throw new EntityNotFoundException($"User with id {userId} does not exist.");
			return mapper.Map<IEnumerable<ReviewDTO>>(reviews);
		}

		public static double CalculateMovieRating(Movie movie, int reviewRatingDifference)
		{
			if (movie.ReviewsCount == 0)
			{
				return 0;
			}
			int ratingsSum = movie.Reviews == null ? 0 : movie.Reviews.Sum(r => r.Rating);
			ratingsSum += reviewRatingDifference;
			return (double)ratingsSum / movie.ReviewsCount;
		}
	}
}
