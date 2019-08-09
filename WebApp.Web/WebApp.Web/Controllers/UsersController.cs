using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.Domain;
using WebApp.Web.Models;

namespace WebApp.Web.Controllers
{
    public class UsersController : Controller
    {
        private UserManager<WebAppUser> _userManager;
        public UsersController(UserManager<WebAppUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Profile()
        {
            string currentUser = User.Identity.Name;

            UserBindingModel model = new UserBindingModel(currentUser,
                new List<UserScore>() {
                     new UserScore ()
                     {
                         CurrentDate = DateTime.Now.ToShortDateString(),
                         Score = 1
                     },
                     new UserScore ()
                     {
                         CurrentDate = DateTime.Now.ToShortDateString(),
                         Score = 2.5
                     },
                     new UserScore ()
                     {
                         CurrentDate = DateTime.Now.ToShortDateString(),
                         Score = 0.5
                     }
            });

            return View("Profile", model);
        }
    }
}