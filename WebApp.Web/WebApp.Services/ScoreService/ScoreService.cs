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
        private readonly IScoreRepository score;
        private readonly UserManager<WebAppUser> userManager;

        public ScoreService(IScoreRepository score, UserManager<WebAppUser> userManager)
        {
            this.score = score;
            this.userManager = userManager;
        }

        public IEnumerable<Rating> GetAllData()
        {
            return score.GetAllData().ToList();
        }

        public Dictionary<string, int> SortedRankList()
        {
            Dictionary<string, int> rankList = new Dictionary<string, int>();

            var rating = GetUsersRating(out var users);

            GetRankList(users, rating, rankList);

            var sortedRankList = GetSortedRankList(rankList);

            return sortedRankList;
        }

        private static Dictionary<string, int> GetSortedRankList(Dictionary<string, int> rankList)
        {
            var orderedRanks = from entry in rankList orderby entry.Value descending select entry;

            Dictionary<string, int> sortedRankList = new Dictionary<string, int>();

            foreach (var item in orderedRanks)
            {
                sortedRankList.Add(item.Key, item.Value);
            }

            return sortedRankList;
        }

        private void GetRankList(List<string> users, List<Rating> rating, Dictionary<string, int> rankList)
        {
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
        }

        private List<Rating> GetUsersRating(out List<string> users)
        {
            List<Rating> rating = GetAllData().ToList();

            users = new List<string>();

            foreach (var item in rating)
            {
                if (!users.Exists(u => u == item.ReceiverId))
                    users.Add(item.ReceiverId);
            }

            return rating;
        }
    }
}
