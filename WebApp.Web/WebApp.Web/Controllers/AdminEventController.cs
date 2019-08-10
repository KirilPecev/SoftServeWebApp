using Microsoft.AspNetCore.Mvc;

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