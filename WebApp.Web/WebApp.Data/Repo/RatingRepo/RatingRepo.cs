namespace WebApp.Data.Repo.RatingRepo
{
    using Domain;
    using GenericRepository;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class RatingRepo : Repository<Rating>, IRatingRepo
    {
        private readonly WebAppDbContext dbContext;

        public RatingRepo(WebAppDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<Rating> GetAllRatings()
        {
            return dbSet.ToList();
        }

        public void AddRating(int eventID, string giverId, string recieverId, int score, DateTime time)
        {
            if (GetAllRatings().ToList().Exists(r => r.GiverId == giverId && r.ReceiverId == recieverId && r.EventId == eventID))
            {
                return;
            }
            else
            {
                MakeNewRating(eventID, giverId, recieverId, score, time);
            }

            dbContext.SaveChanges();
        }

        private void MakeNewRating(int eventID, string giverId, string recieverId, int score, DateTime time)
        {
            Rating newRating = new Rating();
            newRating.GiverId = giverId;
            newRating.ReceiverId = recieverId;
            newRating.EventId = eventID;

            Score newScore = new Score();
            newScore.DateTime = time;
            newScore.Points = score;
            newRating.Scores.Add(newScore);
            dbSet.Add(newRating);
        }
    }
}