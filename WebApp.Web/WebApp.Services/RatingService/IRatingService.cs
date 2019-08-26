namespace WebApp.Services.RatingService
{
    using Domain;
    using System;
    using System.Collections.Generic;

    public interface IRatingService
    {
        IEnumerable<Rating> GetAllRatings();

        void AddRating(int eventId, string giverId, string receiverId, int score, DateTime time);
    }
}