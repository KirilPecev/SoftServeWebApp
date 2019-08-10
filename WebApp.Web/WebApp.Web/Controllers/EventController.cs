using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.Domain;
using WebApp.Services;
using WebApp.Services.EventService;
using WebApp.Web.Models.Event;

namespace WebApp.Web.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventService _eventService;
        private readonly UserManager<WebAppUser> _userManager;
        private readonly ImageService _imageService;

        public EventController(IEventService eventService, UserManager<WebAppUser> userManager)
        {
            this._eventService = eventService;
            this._userManager = userManager;
            this._imageService = new ImageService();
        }
        public IActionResult ViewEvent()
        {
            Event viewEvent = this._eventService.GetEvent(21);
            EventBindingModel model = new EventBindingModel();

            model.AdminId = viewEvent.AdminId;
            model.ImageURL = _imageService.GetImageURL(viewEvent.Image);
            model.Title = viewEvent.Name;
            model.Time = viewEvent.Time;
            
            return View();
        }
    }
}