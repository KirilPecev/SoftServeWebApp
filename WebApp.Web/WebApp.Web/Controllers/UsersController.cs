using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.Domain;
using WebApp.Services.RatingService;
using WebApp.Web.Models;
using System.Linq;

namespace WebApp.Web.Controllers
{
    public class UsersController : Controller
    {
        private UserManager<WebAppUser> _userManager;
        private IRatingService ratingService;

        public UsersController(UserManager<WebAppUser> userManager, IRatingService _ratingService)
        {
            _userManager = userManager;
            ratingService = _ratingService;
        }

        public IActionResult Profile()
        {
            string currentUser = User.Identity.Name;
            double _score = ratingService.GetAllRatings().Where(r => r.Receiver.UserName == currentUser).First().Score;

            UserBindingModel model = new UserBindingModel(currentUser,
                new List<UserScore>() {
                     new UserScore ()
                     {
                         CurrentDate = DateTime.Now.ToShortDateString(),
                         Score = _score
                     }
            });

            return View("Profile", model);
        }
    }
}