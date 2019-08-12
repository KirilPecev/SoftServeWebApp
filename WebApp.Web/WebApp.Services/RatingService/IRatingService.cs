using System;
using System.Collections.Generic;
using System.Text;
using WebApp.Domain;

namespace WebApp.Services.RatingService
{
    public interface IRatingService
    {
        List<Rating> GetAllRatings();
        void AddRating(int eventId, string giverId, string recieverId, int score, DateTime time);
    }
}
