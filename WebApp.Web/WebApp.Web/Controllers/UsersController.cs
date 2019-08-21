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

        public UsersController(UserManager<WebAppUser> userManager, IScoreService _scoreService)
        {
            this.userManager = userManager;
            scoreService = _scoreService;
        }

        [Authorize]
        public IActionResult Profile(string name)
        {
            string currentUser = name;
            string ID = this.userManager.Users.FirstOrDefault(u => u.UserName == currentUser).Id;
            List<Rating> rating = scoreService.GetAllData().Where(rid => rid.ReceiverId == ID).ToList();

            UserBindingModel model = new UserBindingModel();

            model.Name = currentUser;

            foreach (var item in rating)
            {
                foreach (var score in item.Scores)
                {
                    model.Score.Add(new UserScore()
                    {
                        CurrentDate = score.DateTime.ToShortDateString(),
                        Score = score.Points
                    });
                }
            }

            return View("Profile", model);
        }
    }
}