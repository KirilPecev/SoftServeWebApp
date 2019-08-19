namespace WebApp.Web.Controllers
{
    using Domain;
    using Mappers;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.DependencyInjection;
    using Models.Event;
    using Newtonsoft.Json;
    using Scheduler.Scheduler;
    using Services.EventService;
    using System;
    using Microsoft.AspNetCore.Authorization;

    public class HomePageController : Controller
    {
        private readonly IEventService eventService;
        private readonly IEventMapper eventMapper;
        private readonly IServiceProvider provider;
        private readonly IServiceScopeFactory factory;
        private readonly IDistributedCache distributedCache;
        private readonly UserManager<WebAppUser> userManager;

        public HomePageController(IServiceProvider provider, IServiceScopeFactory factory, IDistributedCache distributedCache, UserManager<WebAppUser> userManager, IEventService eventService, IEventMapper eventMapper)
        {
            this.provider = provider;
            this.factory = factory;
            this.distributedCache = distributedCache;
            this.userManager = userManager;
            this.eventService = eventService;
            this.eventMapper = eventMapper;
        }

        public IActionResult HomePageView()
        {

            return ReturnMainView();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult HomePageView(EventBindingModel model, IFormFile eventImage)
        {
            this.eventService.CreateEvent(eventMapper.MapEventToDB(model, eventImage, userManager.GetUserId(User)));
            var task = new EventsTask(factory);
            task.ProcessInScope(provider);

            return ReturnMainView();
        }

        [Authorize]
        public IActionResult DetermineEventView(int Id)
        {
            Event dbEvent = eventService.GetEvent(Id);

            if (dbEvent.AdminId == userManager.GetUserId(User))
                return RedirectToAction("AdminViewEvent", "AdminEvent", dbEvent);
            else
                return RedirectToAction("ViewEvent", "Event", dbEvent);
        }

        [Authorize]
        private ViewResult ReturnMainView()
        {
            HomePageBinding model = new HomePageBinding();

            var events = this.distributedCache.GetString("events");
            var deserializedEvents = JsonConvert.DeserializeObject<Event[]>(events);

            foreach (var dbEvent in deserializedEvents)
            {
                model.Events.Add(eventMapper.MapDbToEvent(dbEvent));
            }

            return View(model);
        }
    }
}