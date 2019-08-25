namespace WebApp.Web.Controllers
{
    using Domain;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Services.ScoreService;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class RankListController : Controller
    {
        private readonly IScoreService scoreService;
        private readonly UserManager<WebAppUser> userManager;
        public RankListController(IScoreService _scoreService, UserManager<WebAppUser> userManager)
        {
            this.userManager = userManager;
            scoreService = _scoreService;
        }

        [Authorize]
        public IActionResult CurrentRankList()
        {
            Dictionary<string, int> ranklist = new Dictionary<string, int>();
            List<Rating> rating = scoreService.GetAllData();

            List<string> users = new List<string>();

            foreach (var item in rating)
            {
                if(!users.Exists(u => u == item.ReceiverId))
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
                ranklist.Add(userName, (int)Math.Round(average));
            }

            var sortedRankList = from entry in ranklist orderby entry.Value descending select entry;

            Dictionary<string, int> _sortedRankList = new Dictionary<string, int>();

            foreach (var item in sortedRankList)
            {
                _sortedRankList.Add(item.Key, item.Value);
            }

            return View(_sortedRankList);
        }
    }
}