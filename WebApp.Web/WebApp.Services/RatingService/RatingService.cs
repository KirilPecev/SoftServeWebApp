namespace WebApp.Services.RatingService
{
    using Data.Repo.RatingRepo;
    using Domain;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class RatingService : IRatingService
    {
        private readonly IRatingRepository rating;

        public RatingService(IRatingRepository rating)
        {
            this.rating = rating;
        }

        public IEnumerable<Rating> GetAllRatings()
        {
            return rating.GetAllRatings().ToList();
        }

        public void AddRating(int eventId, string giverId, string receiverId, int score, DateTime time)
        {
            this.rating.AddRating(eventId, giverId, receiverId, score, time);
        }
    }
}