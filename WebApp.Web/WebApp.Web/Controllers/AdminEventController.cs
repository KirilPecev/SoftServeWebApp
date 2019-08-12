using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Domain;
using WebApp.Services.EventService;
using WebApp.Web.Controllers.Mappers;
using WebApp.Web.Models.Event;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using WebApp.Services.EventAttendance;

namespace WebApp.Web.Controllers
{
    public class AdminEventController : Controller
    {
        private readonly UserManager<WebAppUser> _userManager;
        private readonly IEventService _eventService;
        private readonly IEventMapper eventMapper;
        private readonly IEventAttendanceService attendanceService;

        public AdminEventController(UserManager<WebAppUser> userManager, IEventService eventService, IEventMapper eventMapper, IEventAttendanceService attendanceService)
        {
            this._userManager = userManager;
            this._eventService = eventService;
            this.eventMapper = eventMapper;
            this.attendanceService = attendanceService;
        }

        [HttpGet]
        public IActionResult AdminViewEvent(Event dbEvent)
        {
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

        [HttpGet]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            await _eventService.DeleteEvent(id);

            //TODO: pop with message of success or not

          return RedirectToAction(nameof(GetMyEvents));
        }
        public IActionResult AprooveUser(IDictionary<string, string> rv)
        {
            int eventId = int.Parse(rv["eventId"]);
            int positionId = int.Parse(rv["positionId"]);
            string userID = rv["userId"];
            attendanceService.ApproveUserForeEvent(userID, eventId, positionId).Wait();
            return RedirectToAction("HomePageView", "HomePage");
        }
        public IActionResult IgnoreUser(IDictionary<string, string> rv)
        {
            int eventId = int.Parse(rv["eventId"]);
            int positionId = int.Parse(rv["positionId"]);
            string userID = rv["userId"];
            attendanceService.RemoveUserAtendeeToBeAprooved(userID, eventId, positionId);
            return RedirectToAction("HomePageView", "HomePage");
        }
    }
}