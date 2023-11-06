using DataLayer.Models;

namespace DataLayer.Services;

public class ReviewService : IReviewService
{
    private readonly ApplicationDbContext _ApplicationDbContext;

    public ReviewService(ApplicationDbContext melodyMineService)
    {
        _ApplicationDbContext = melodyMineService;
    }
    
    public IQueryable<Review> getReviewsByVinylID(int vinylId)
    {
        IQueryable<Review> tempReviews = _ApplicationDbContext.Reviews
            .Where(p => p.VinylId == vinylId);
        return tempReviews;
    }
    
    public void CreateReview(Review review)
    {
        _ApplicationDbContext.Reviews.Add(review);

        _ApplicationDbContext.SaveChanges();
    }
}