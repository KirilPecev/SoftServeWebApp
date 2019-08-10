using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApp.Services.EventService;
using WebApp.Web.Controllers.Mappers;
using WebApp.Web.Models.Event;

namespace WebApp.Web.Controllers
{
    public class HomePageController : Controller
    {
        private readonly IEventService _eventService;
        private readonly IEventMapper eventMapper;

        public HomePageController(IEventService eventService, IEventMapper eventMapper)
        {
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
            this._eventService.CreateEvent(eventMapper.MapEventToDB(model,eventImage));
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