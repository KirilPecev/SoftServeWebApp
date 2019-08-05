using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Web.Models.Event;

namespace WebApp.Web.Controllers
{
    public class HomePageController : Controller
    {
        public IActionResult HomePageView()
        {
            // request to the db
            return ReturnMainView();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult HomePageView(EventBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = string.Empty;
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    errorMessages += error;
                }
                return BadRequest(errorMessages);
            }
            // save intothe db
            // 
            /*return this.Json(model);*/
            return ReturnMainView();
        }

        private ViewResult ReturnMainView()
        {
            HomePageBinding model = new HomePageBinding();
            return View(model);
        }
    }
}