using System.Collections.Generic;
using System.Linq;
using WebApp.Domain;

namespace WebApp.Data.Repo
{
    public class RatingRepo : Repository<Rating>, IRatingRepo
    {
        private readonly WebAppDbContext dbContext;

        public RatingRepo(WebAppDbContext dbContext) : base(dbContext)
        {
        }

        public List<Rating> GetAllRatings()
        {
            return dbSet.ToList();
        }
        
        public void AddRating(Rating rating)
        {
            dbSet.Add(rating);
            dbContext.SaveChanges();
        }
    }
}
