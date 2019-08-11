using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.Domain;
using WebApp.Web.Models;
using System.Linq;

namespace WebApp.Web.Controllers
{
    using System.Security.Claims;
    using WebApp.Services.ScoreService;

    public class UsersController : Controller
    {
        private UserManager<WebAppUser> _userManager;
        private IScoreService scoreService;

        public UsersController(UserManager<WebAppUser> userManager, IScoreService _scoreService)
        {
            _userManager = userManager;
            scoreService = _scoreService;
        }

        public IActionResult Profile(string name)
        {
            string currentUser = name;
            string ID = this._userManager.Users.FirstOrDefault(u => u.UserName == currentUser).Id;
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