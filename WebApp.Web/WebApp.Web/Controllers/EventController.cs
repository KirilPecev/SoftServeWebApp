using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApp.Domain;
using WebApp.Services.EventAttendance;
using WebApp.Services.EventService;
using WebApp.Services.RatingService;
using WebApp.Web.Controllers.Mappers;

namespace WebApp.Web.Controllers
{
    public class EventController : Controller
    {
        private Event current;
        private readonly IEventMapper eventMapper;
        private readonly UserManager<WebAppUser> userManager;
        private readonly IEventAttendanceService attendanceService;
        private readonly IRatingService ratingService;
        public EventController(UserManager<WebAppUser> userManager, IEventMapper eventMapper, IEventAttendanceService attendanceService, IRatingService ratingService)
        {
            this.ratingService = ratingService;
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
            if(!attendanceService.GetAllEventAttendeesForEvent(eventId).Any(a => a.UserId == joinID))
               attendanceService.RegisterUserForEvent(joinID, eventId, positionId).Wait();

            return RedirectToAction("HomePageView", "HomePage");
        }
        public IActionResult AddRating(IDictionary<string, string> rv)
        {
            string giverId = userManager.GetUserId(User);
            string recieverId = rv["recieverId"];
            int score = int.Parse(rv["rating"]);
            int eventId = int.Parse(rv["eventID"]);
            ratingService.AddRating(eventId,giverId,recieverId,score,DateTime.Now);
            return RedirectToAction("HomePageView", "HomePage");
        }
    }
}