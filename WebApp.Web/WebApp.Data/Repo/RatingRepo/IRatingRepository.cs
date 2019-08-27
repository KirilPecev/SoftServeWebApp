namespace WebApp.Data.Repo.RatingRepo
{
    using Domain;
    using System;
    using System.Collections.Generic;

    public interface IRatingRepository
    {
        IEnumerable<Rating> GetAllRatings();

        void AddRating(int eventId, string giverId, string recieverId, int score, DateTime time);
    }
}