namespace WebApp.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Notifications;
    using System.Diagnostics;

    public class HomeController : Controller
    {
        private IEmailSender emailSender;

        public HomeController(IEmailSender emailSender)
        {
            this.emailSender = emailSender;
        }

        public IActionResult Index()
        {
            this.emailSender.SendEmailAsync("softserveapp@abv.bg", "Sport app init", "On Init").Wait();

            if (!this.User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login");
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
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
