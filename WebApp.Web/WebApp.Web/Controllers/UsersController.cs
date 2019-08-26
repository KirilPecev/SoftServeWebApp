namespace WebApp.Web.Controllers
{
    using Domain;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services.ScoreService;
    using System.Collections.Generic;
    using System.Linq;

    public class UsersController : Controller
    {
        private readonly UserManager<WebAppUser> userManager;
        private readonly IScoreService scoreService;

        public UsersController(UserManager<WebAppUser> userManager, IScoreService scoreService)
        {
            this.userManager = userManager;
            this.scoreService = scoreService;
        }

        [Authorize]
        public IActionResult Profile(string name)
        {
            string id = this.userManager.Users.FirstOrDefault(u => u.UserName == name).Id;
            List<Rating> rating = scoreService.GetAllData().Where(rid => rid.ReceiverId == id).ToList();
            List<UserScore> userScores = new List<UserScore>();

            UserBindingModel model = new UserBindingModel();

            model.Name = name;

            foreach (var item in rating)
            {
                foreach (var score in item.Scores)
                {
                    userScores.Add(new UserScore()
                    {
                        CurrentDate = score.DateTime.ToShortDateString(),
                        Score = score.Points
                    });
                }
            }

            model.Score = userScores;

            return View("Profile", model);
        }
    }
}