using System.Collections.Generic;
using WebApp.Domain;

namespace WebApp.Data.Repo
{
    public interface IRatingRepo
    {
        List<Rating> GetAllRatings();
        void AddRating(Rating rating);
    }
}
