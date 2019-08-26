namespace WebApp.Web.Controllers
{
    using Domain;
    using Mappers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using Models;
    using Models.Event;
    using Services.EventAttendance;
    using Services.EventService;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using WebApp.Scheduler.Scheduler;

    public class AdminEventController : Controller
    {
        private readonly UserManager<WebAppUser> userManager;
        private readonly IEventService eventService;
        private readonly IEventMapper eventMapper;
        private readonly IEventAttendanceService attendanceService;
        private readonly IServiceProvider provider;
        private readonly IServiceScopeFactory factory;

        public AdminEventController(IServiceProvider provider, IServiceScopeFactory factory,
            UserManager<WebAppUser> userManager, IEventService eventService, IEventMapper eventMapper, IEventAttendanceService attendanceService)
        {
            this.userManager = userManager;
            this.eventService = eventService;
            this.eventMapper = eventMapper;
            this.attendanceService = attendanceService;
            this.provider = provider;
            this.factory = factory;
        }

        [HttpGet]
        [Authorize]
        public IActionResult AdminViewEvent(Event dbEvent)
        {
            var model = eventMapper.ViewEvent(dbEvent);
            return View(model);
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetMyEvents()
        {
            var userId = userManager.GetUserId(User);

            if (userId == null)
            {
                return BadRequest("No such User registered!");
            }

            var allMappedEventsByUser = new List<EventBindingModel>();

            var allEventsByUser = eventService.GetAllEventsByUser(userId);

            foreach (var eventByUser in allEventsByUser)
            {
                allMappedEventsByUser.Add(eventMapper.ViewEvent(eventByUser));
            }

            return View(allMappedEventsByUser);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            await eventService.DeleteEvent(id);

            var task = new EventsTask(factory);
            await task.ProcessInScope(provider);

            return RedirectToAction(nameof(GetMyEvents));
        }

        [Authorize]
        public IActionResult Edit(EventBindingModel model, IFormFile eventImage)
        {
            var viewModel = eventMapper.ModifiedEvent(model, eventImage, userManager.GetUserId(User));
            this.eventService.EditEvent(viewModel);

            var task = new EventsTask(factory);
            task.ProcessInScope(provider);

            return this.RedirectToAction("GetMyEvents");
        }

        [Authorize]
        public IActionResult ApproveUser(ApproveOrIgnoreUserModel model)
        {
            int eventId = model.EventId;
            int positionId = model.PositionId;
            string userId = model.UserId;

            attendanceService.ApproveUserForeEvent(userId, eventId, positionId).Wait();
            return RedirectToAction("DetermineEventView", "HomePage", new { Id = eventId });
        }

        [Authorize]
        public IActionResult IgnoreUser(ApproveOrIgnoreUserModel model)
        {
            int eventId = model.EventId;
            int positionId = model.PositionId;
            string userId = model.UserId;

            attendanceService.RemoveUserAttendeeToBeApproved(userId, eventId, positionId);
            return RedirectToAction("DetermineEventView", "HomePage", new { Id = eventId });
        }
    }
}