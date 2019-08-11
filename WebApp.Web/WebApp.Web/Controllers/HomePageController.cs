namespace WebApp.Web.Controllers
{
    using Domain;
    using Mappers;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Distributed;
    using Models.Event;
    using Newtonsoft.Json;
    using Services.EventService;

    public class HomePageController : Controller
    {
        private readonly IEventService _eventService;
        private readonly IEventMapper eventMapper;
        private readonly IDistributedCache distributedCache;
        private readonly UserManager<WebAppUser> _userManager;

        public HomePageController(IDistributedCache distributedCache, UserManager<WebAppUser> userManager, IEventService eventService, IEventMapper eventMapper)
        {
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