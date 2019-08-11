using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
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
        public IActionResult AdminViewEvent(int id)
        {
            Event dbEvent = this._eventService.GetEvent(id);
            return View(eventMapper.MapDbToEvent(dbEvent));
        }

        public async Task<IActionResult> GetMyEvents(string adminId)
        {


            return null;
        }
    }
}