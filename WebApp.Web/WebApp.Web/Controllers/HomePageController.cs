namespace WebApp.Web.Controllers
{
    using Domain;
    using Mappers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.DependencyInjection;
    using Models.Event;
    using Newtonsoft.Json;
    using Services.EventService;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class HomePageController : BaseController
    {
        private readonly IEventService eventService;
        private readonly IEventMapper eventMapper;
        private readonly IServiceProvider provider;
        private readonly IServiceScopeFactory factory;
        private readonly IDistributedCache distributedCache;
        private readonly UserManager<WebAppUser> userManager;

        public HomePageController(
            IServiceProvider provider,
            IServiceScopeFactory factory,
            IDistributedCache distributedCache,
            UserManager<WebAppUser> userManager,
            IEventService eventService,
            IEventMapper eventMapper) : base(provider, factory)
        {
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
        public async Task<IActionResult> HomePageView(EventBindingModel model, IFormFile eventImage)
        {
            this.eventService.CreateEvent(eventMapper.NewEvent(model, eventImage, userManager.GetUserId(User)));

            await base.UpdateEventsInCache();

            return ReturnMainView();
        }

        [Authorize]
        public IActionResult DetermineEventView(int Id)
        {
            Event currentEvent = eventService.GetEvent(Id);

            if (currentEvent.AdminId == userManager.GetUserId(User))
                return RedirectToAction("AdminViewEvent", "AdminEvent", currentEvent);
            else
                return RedirectToAction("ViewEvent", "Event", currentEvent);
        }

        [Authorize]
        private IActionResult ReturnMainView()
        {
            HomePageBinding model = new HomePageBinding();

            var events = GetEvents();

            foreach (var currentEvent in events)
            {
                if (!currentEvent.IsDeleted)
                    model.Events.Add(eventMapper.ViewEvent(currentEvent));
            }

            return View(model);
        }

        public IActionResult SearchForEvent(string searchedEvent)
        {
            HomePageBinding model = new HomePageBinding();

            var events = GetEvents();

            if (!string.IsNullOrEmpty(searchedEvent))
            {
                events = events.Where(e => e.Location.Contains(searchedEvent)).ToArray();
            }

            foreach (var currentEvent in events)
            {
                model.Events.Add(eventMapper.ViewEvent(currentEvent));
            }

            return View("HomePageView", model);
        }

        private Event[] GetEvents()
        {
            var events = this.distributedCache.GetString("events");
            return JsonConvert.DeserializeObject<Event[]>(events);
        }
    }
}