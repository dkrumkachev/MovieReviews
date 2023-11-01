using BusinessLayer.Models.Review;

namespace BusinessLayer.Services.Contracts
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
