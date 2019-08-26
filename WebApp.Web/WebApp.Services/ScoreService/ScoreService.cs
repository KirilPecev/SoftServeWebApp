namespace WebApp.Services.ScoreService
{
    using Data.Repo.ScoreRepo;
    using Domain;
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ScoreService : IScoreService
    {
        private readonly IScoreRepo scores;
        private readonly UserManager<WebAppUser> userManager;

        public ScoreService(IScoreRepo scores, UserManager<WebAppUser> userManager)
        {
            this.scores = scores;
            this.userManager = userManager;
        }

        public IEnumerable<Rating> GetAllData()
        {
            return scores.GetAllData().ToList();
        }

        public Dictionary<string, int> SortedRankList()
        {
            Dictionary<string, int> rankList = new Dictionary<string, int>();
            List<Rating> rating = GetAllData().ToList();

            List<string> users = new List<string>();

            foreach (var item in rating)
            {
                if (!users.Exists(u => u == item.ReceiverId))
                    users.Add(item.ReceiverId);
            }

            foreach (var user in users)
            {
                var userRatings = rating.Where(r => r.ReceiverId == user).ToList();
                double average = 0;
                int count = 0;
                foreach (var item in userRatings)
                {
                    average += item.Scores.First().Points;
                    count++;
                }

                average /= count;
                string userName = userManager.Users.FirstOrDefault(u => u.Id == user).UserName;
                rankList.Add(userName, (int)Math.Round(average));
            }

            var orderedRanks = from entry in rankList orderby entry.Value descending select entry;

            Dictionary<string, int> sortedRankList = new Dictionary<string, int>();

            foreach (var item in orderedRanks)
            {
                sortedRankList.Add(item.Key, item.Value);
            }

            return sortedRankList;
        }
    }
}
