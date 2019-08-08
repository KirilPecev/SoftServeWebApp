using Microsoft.AspNetCore.Mvc;

namespace WebApp.Web.Controllers
{
    public class EventController : Controller
    {
        public IActionResult ViewEvent()
        {
            return View();
        }
    }
}