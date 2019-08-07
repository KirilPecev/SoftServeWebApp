using Microsoft.AspNetCore.Mvc;
using System;

namespace WebApp.Web.Controllers
{
    public class AdminEventController : Controller
    {
        [HttpGet]
        public IActionResult AdminViewEvent()
        {
            return View();
        }
    }
}
