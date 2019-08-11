using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Domain;
using WebApp.Services.EventService;
using WebApp.Web.Controllers.Mappers;
using WebApp.Web.Models.Event;

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
        public IActionResult AdminViewEvent(int id)
        {
            Event dbEvent = this._eventService.GetEvent(id);
            return View(eventMapper.MapDbToEvent(dbEvent));
        }

        [HttpGet]
        public IActionResult GetMyEvents(string id)
        {
            var allMappedEventsByUser = new List<EventBindingModel>();

            var allEventsByUser = _eventService.GetAllEventsByUser(id);

            foreach (var eventByUser in allEventsByUser)
            {
                allMappedEventsByUser.Add(eventMapper.MapDbToEvent(eventByUser));
            }

            return View(allMappedEventsByUser);
        }
    }
}