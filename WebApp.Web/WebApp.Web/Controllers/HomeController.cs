namespace WebApp.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using System.Diagnostics;

    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login");
            }

            return this.RedirectToAction("HomePageView","HomePage");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult FirstTimeIntoApp()
        {
            return View();
        }
    }
}
