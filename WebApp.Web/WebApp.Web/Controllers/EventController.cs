using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.Domain;
using WebApp.Services.EventService;
using WebApp.Web.Controllers.Mappers;

namespace WebApp.Web.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventMapper eventMapper;
        private readonly UserManager<WebAppUser> userManager;
        public EventController(UserManager<WebAppUser> userManager, IEventMapper eventMapper)
        {
            this.userManager = userManager;
            this.eventMapper = eventMapper;
        }
        public IActionResult ViewEvent(Event dbEvent)
        {
            return View(eventMapper.MapDbToEvent(dbEvent));
        }
        public IActionResult JoinUser()
        {
            string joinID = this.userManager.GetUserId(User);
            return View();
        }
    }
}