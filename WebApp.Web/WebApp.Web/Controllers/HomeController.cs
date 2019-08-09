namespace WebApp.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Distributed;
    using Models;
    using Notifications;
    using System.Diagnostics;

    public class HomeController : Controller
    {
        private IEmailSender emailSender;
        private readonly IDistributedCache distributedCache;

        public HomeController(IEmailSender emailSender, IDistributedCache distributedCache)
        {
            this.emailSender = emailSender;
            this.distributedCache = distributedCache;
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

        public IActionResult Test()
        {
            var events = this.distributedCache.GetString("events");

            return this.Content(events);
        }
    }
}
