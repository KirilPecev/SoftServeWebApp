namespace WebApp.Web.Controllers
{
    using Domain;
    using Mappers;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using Models.Event;
    using Scheduler.Scheduler;
    using Services.EventService;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;

    public class AdminEventController : Controller
    {
        private readonly UserManager<WebAppUser> _userManager;
        private readonly IEventService _eventService;
        private readonly IEventMapper eventMapper;
        private readonly IServiceProvider provider;
        private readonly IServiceScopeFactory factory;

        public AdminEventController(IServiceProvider provider, IServiceScopeFactory factory, UserManager<WebAppUser> userManager, IEventService eventService, IEventMapper eventMapper)
        {
            this._userManager = userManager;
            this._eventService = eventService;
            this.eventMapper = eventMapper;
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

            return this.Ok();
        }
    }
}