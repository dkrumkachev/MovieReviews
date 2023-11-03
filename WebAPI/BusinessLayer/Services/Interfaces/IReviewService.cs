using BusinessLayer.Models.Review;

namespace BusinessLayer.Services.Interfaces
{
	public interface IReviewService
	{
		ReviewDTO Create(ReviewCreateDTO review);

		ReviewDTO Update(int reviewId, ReviewUpdateDTO review);

		void Delete(int reviewId);

		ReviewDTO GetById(int reviewId);

		IEnumerable<ReviewDTO> GetByUser(int userId);
	}
}
