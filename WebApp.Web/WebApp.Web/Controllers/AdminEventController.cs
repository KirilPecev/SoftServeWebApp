namespace WebApp.Web.Controllers
{
    using Domain;
    using Mappers;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
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
        public IActionResult AdminViewEvent(Event dbEvent)
        {
            var model = eventMapper.MapDbToEvent(dbEvent);
            return View(model);
        }

        [HttpGet]
        public IActionResult GetMyEvents()
        {
            var userId = userManager.GetUserId(User);

            if (userId == null)
            {
                return BadRequest("No such User registerd");
            }

            var allMappedEventsByUser = new List<EventBindingModel>();

            var allEventsByUser = eventService.GetAllEventsByUser(userId);

            foreach (var eventByUser in allEventsByUser)
            {
                allMappedEventsByUser.Add(eventMapper.MapDbToEvent(eventByUser));
            }

            return View(allMappedEventsByUser);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            await eventService.DeleteEvent(id);

            var task = new EventsTask(factory);
            await task.ProcessInScope(provider);

            //TODO: pop with message of success or not

            return RedirectToAction(nameof(GetMyEvents));
        }

        public IActionResult Edit(EventBindingModel model, IFormFile eventImage)
        {
            var viewModel = eventMapper.MapEditEventToDB(model, eventImage, userManager.GetUserId(User));
            this.eventService.EditEvent(viewModel);

            var task = new EventsTask(factory);
            task.ProcessInScope(provider);

            return this.RedirectToAction("GetMyEvents");
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