using Microsoft.AspNetCore.Mvc;
using WebApp.Domain;
using WebApp.Services.EventService;
using WebApp.Web.Controllers.Mappers;

namespace WebApp.Web.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventService _eventService;
        private readonly IEventMapper eventMapper;

        public EventController(IEventService eventService, IEventMapper eventMapper)
        {
            this._eventService = eventService;
            this.eventMapper = eventMapper;
        }
        public IActionResult ViewEvent(int Id)
        {
            Event dbEvent = this._eventService.GetEvent(Id);
            return View(eventMapper.MapDbToEvent(dbEvent));
        }
    }
}