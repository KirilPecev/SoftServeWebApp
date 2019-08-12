
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
using System;
using WebApp.Scheduler.Scheduler;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace WebApp.Web.Controllers
{ 
    public class AdminEventController : Controller
    {
        private readonly UserManager<WebAppUser> _userManager;
        private readonly IEventService _eventService;
        private readonly IEventMapper eventMapper;
        private readonly IEventAttendanceService attendanceService;
        private readonly IServiceProvider provider;
        private readonly IServiceScopeFactory factory;

        public AdminEventController(IServiceProvider provider, IServiceScopeFactory factory, 
            UserManager<WebAppUser> userManager, IEventService eventService, IEventMapper eventMapper, IEventAttendanceService attendanceService)
        {
            this._userManager = userManager;
            this._eventService = eventService;
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

            var task = new EventsTask(factory);
            task.ProcessInScope(provider);

            //TODO: pop with message of success or not

            return RedirectToAction(nameof(GetMyEvents));
        }

        public IActionResult Edit(EventBindingModel model, IFormFile eventImage)
        {
            var viewModel = eventMapper.MapEditEventToDB(model, eventImage, _userManager.GetUserId(User));
            this._eventService.EditEvent(viewModel);

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