namespace WebApp.Data.Repo.ScoreRepo
{
    using Domain;
    using GenericRepository;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ScoreRepo : Repository<Rating>, IScoreRepo
    {
        public ScoreRepo(WebAppDbContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<Rating> GetAllData()
        {
            return dbSet
                .Include(x => x.Scores)
                .ToList();
        }

        public IEnumerable<int> GetScores(List<int> scores)
        {
            return scores;
        }

        public IEnumerable<string> GetDates(List<DateTime> dates)
        {
            List<string> ddates = new List<string>();

            foreach (var item in dates)
            {
                ddates.Add(item.ToShortDateString());
            }

            return ddates;
        }
    }
}
