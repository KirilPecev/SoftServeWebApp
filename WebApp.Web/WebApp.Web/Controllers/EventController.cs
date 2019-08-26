namespace WebApp.Web.Controllers
{
    using Domain;
    using Mappers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Services.EventAttendance;
    using Services.RatingService;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Models;

    public class EventController : Controller
    {
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

        [Authorize]
        public IActionResult ViewEvent(Event dbEvent)
        {
            return View(eventMapper.ViewEvent(dbEvent));
        }

        [Authorize]
        public IActionResult JoinUser(JoinUserModel model)
        {
            int eventId = model.EventId;
            int positionId = model.PositionId;
            string joinID = this.userManager.GetUserId(User);
            if (!attendanceService.GetAllEventAttendeesForEvent(eventId).Any(a => a.UserId == joinID))
                attendanceService.RegisterUserForEvent(joinID, eventId, positionId).Wait();

            return RedirectToAction("DetermineEventView", "HomePage", new { Id = eventId });
        }

        public IActionResult AddRating(IDictionary<string, string> rv)
        {
            string giverId = userManager.GetUserId(User);
            string recieverId = rv["recieverId"];
            int score = int.Parse(rv["rating"]);
            int eventId = int.Parse(rv["eventID"]);
            ratingService.AddRating(eventId, giverId, recieverId, score, DateTime.Now);
            return RedirectToAction("DetermineEventView", "HomePage", new { Id = eventId });
        }
    }
}