namespace WebApp.Web.Controllers
{
    using Domain;
    using Mappers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services.EventAttendanceService;
    using Services.RatingService;
    using System;
    using System.Linq;

    public class EventController : BaseController
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
            string joinId = this.userManager.GetUserId(User);

            if (!attendanceService.GetAllEventAttendeesForEvent(eventId).Any(a => a.UserId == joinId))
                attendanceService.RegisterUserForEvent(joinId, eventId, positionId).Wait();

            return RedirectToAction("DetermineEventView", "HomePage", new { Id = eventId });
        }

        public IActionResult AddRating(RatingModel model)
        {
            string giverId = userManager.GetUserId(User);
            string receiverId = model.ReceiverId;
            int score = model.Rating;
            int eventId = model.EventId;

            ratingService.AddRating(eventId, giverId, receiverId, score, DateTime.Now);

            return RedirectToAction("DetermineEventView", "HomePage", new { Id = eventId });
        }
    }
}