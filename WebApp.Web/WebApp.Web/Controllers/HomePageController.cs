namespace WebApp.Web.Controllers
{
    using System;
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

    public class HomePageController : Controller
    {
        private readonly IEventService _eventService;
        private readonly IEventMapper eventMapper;
        private readonly IServiceProvider provider;
        private readonly IServiceScopeFactory factory;
        private readonly IDistributedCache distributedCache;
        private readonly UserManager<WebAppUser> _userManager;

        public HomePageController(IServiceProvider provider, IServiceScopeFactory factory, IDistributedCache distributedCache, UserManager<WebAppUser> userManager, IEventService eventService, IEventMapper eventMapper)
        {
            this.provider = provider;
            this.factory = factory;
            this.distributedCache = distributedCache;
            this._userManager = userManager;
            this._eventService = eventService;
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
            this._eventService.CreateEvent(eventMapper.MapEventToDB(model, eventImage, _userManager.GetUserId(User)));
            var task = new EventsTask(factory);
            task.ProcessInScope(provider);

            return ReturnMainView();
        }

        public IActionResult DetermineEventView(int Id)
        {
            Event dbEvent = _eventService.GetEvent(Id);

            if (dbEvent.AdminId == _userManager.GetUserId(User))
                return RedirectToAction("AdminViewEvent", "AdminEvent", dbEvent);
            else
                return RedirectToAction("ViewEvent","Event", dbEvent);
        }

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

        [HttpGet]
        public JsonResult GetEvent(object model)
        {
            return Json(model);
        }
    }
}