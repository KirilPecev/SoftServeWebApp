using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Domain;
using WebApp.Services.EventService;
using WebApp.Web.Controllers.Mappers;
using WebApp.Web.Models.Event;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;


namespace WebApp.Web.Controllers
{
    public class AdminEventController : Controller
    {
        private readonly UserManager<WebAppUser> _userManager;
        private readonly IEventService _eventService;
        private readonly IEventMapper eventMapper;

        public AdminEventController(UserManager<WebAppUser> userManager, IEventService eventService, IEventMapper eventMapper)
        {
            this._userManager = userManager;
            this._eventService = eventService;
            this.eventMapper = eventMapper;
        }

        [HttpGet]
        public IActionResult AdminViewEvent(int id)
        {
            Event dbEvent = this._eventService.GetEvent(id);
            return View(eventMapper.MapDbToEvent(dbEvent));
        }

        [HttpGet]
        public IActionResult GetMyEvents()
        {
            var userId = _userManager.GetUserId(User);

            if (userId == null)
            {
                return BadRequest("No such User registerd");
            }

            var allMappedEventsByUser = new List<EventBindingModel>();

            var allEventsByUser = _eventService.GetAllEventsByUser(userId);

            foreach (var eventByUser in allEventsByUser)
            {
                allMappedEventsByUser.Add(eventMapper.MapDbToEvent(eventByUser));
            }

            return View(allMappedEventsByUser);
        }
    }
}