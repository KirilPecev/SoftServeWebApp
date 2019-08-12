using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApp.Domain;
using WebApp.Services.EventAttendance;
using WebApp.Services.EventService;
using WebApp.Web.Controllers.Mappers;

namespace WebApp.Web.Controllers
{
    public class EventController : Controller
    {
        private Event current;
        private readonly IEventMapper eventMapper;
        private readonly UserManager<WebAppUser> userManager;
        private readonly IEventAttendanceService attendanceService;
        public EventController(UserManager<WebAppUser> userManager, IEventMapper eventMapper, IEventAttendanceService attendanceService)
        {
            this.userManager = userManager;
            this.eventMapper = eventMapper;
            this.attendanceService = attendanceService;
        }
        public IActionResult ViewEvent(Event dbEvent)
        {
            this.current = dbEvent;
            return View(eventMapper.MapDbToEvent(dbEvent));
        }
        public IActionResult JoinUser(IDictionary<string,string> rv)
        {
            int eventId = int.Parse(rv["eventId"]);
            int positionId = int.Parse(rv["positionId"]);
            string joinID = this.userManager.GetUserId(User);
            attendanceService.RegisterUserForEvent(joinID, eventId, positionId);
            return RedirectToAction("HomePageView", "HomePage");
        }
    }
}