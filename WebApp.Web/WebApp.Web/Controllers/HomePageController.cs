using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.Domain;
using WebApp.Services.EventService;
using WebApp.Web.Controllers.Mappers;
using WebApp.Web.Models.Event;

namespace WebApp.Web.Controllers
{
    public class HomePageController : Controller
    {
        private readonly IEventService _eventService;
        private readonly IEventMapper eventMapper;
        private readonly UserManager<WebAppUser> _userManager;

        public HomePageController(UserManager<WebAppUser> userManager, IEventService eventService, IEventMapper eventMapper)
        {
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
            this._eventService.CreateEvent(eventMapper.MapEventToDB(model,eventImage,_userManager.GetUserId(User)));
            return ReturnMainView();
        }

        private ViewResult ReturnMainView()
        {
            HomePageBinding model = new HomePageBinding();
            foreach (var dbEvent in _eventService.GetAllEvents())
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