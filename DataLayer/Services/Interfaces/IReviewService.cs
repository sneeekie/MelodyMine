using DataLayer.Models;

namespace DataLayer.Services;

public interface IReviewService
{
    public IQueryable<Review> getReviewsByVinylID(int vinylId);
    public void CreateReview(Review review);
}