using Microsoft.AspNetCore.Mvc;
using WebApp.Domain;
using WebApp.Services.EventService;
using WebApp.Web.Controllers.Mappers;

namespace WebApp.Web.Controllers
{
    public class AdminEventController : Controller
    {
        private readonly IEventService _eventService;
        private readonly IEventMapper eventMapper;

        public AdminEventController(IEventService eventService, IEventMapper eventMapper)
        {
            this._eventService = eventService;
            this.eventMapper = eventMapper;
        }
        [HttpGet]
        public IActionResult AdminViewEvent(Event dbEvent)
        {
            return View(eventMapper.MapDbToEvent(dbEvent));
        }
    }
}