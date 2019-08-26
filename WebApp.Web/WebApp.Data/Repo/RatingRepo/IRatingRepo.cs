namespace WebApp.Data.Repo.RatingRepo
{
    using Domain;
    using System;
    using System.Collections.Generic;

    public interface IRatingRepo
    {
        IEnumerable<Rating> GetAllRatings();

        void AddRating(int eventId, string giverId, string recieverId, int score, DateTime time);
    }
}