using System;
using System.Collections.Generic;
using WebApp.Domain;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Data.Repo
{
    public class ScoreRepo : Repository<Rating>, IScoreRepo
    {
        private readonly WebAppDbContext dbContext;

        public ScoreRepo(WebAppDbContext dbContext) : base(dbContext)
        {
        }

        public List<Rating> GetAllData()
        {
            return dbSet
                .Include(x=>x.Scores)
                .ToList();
        }

        public List<int> GetScores(List<int> scores)
        {
            return scores;
        }

        public List<string> GetDates(List<DateTime> dates)
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
