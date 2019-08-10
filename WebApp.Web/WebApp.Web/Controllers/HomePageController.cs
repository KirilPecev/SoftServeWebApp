using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApp.Domain;
using WebApp.Services.EventService;
using WebApp.Web.Models.Event;
using WebApp.ImageStorage.AzureBlobStorage;
using Microsoft.AspNetCore.Identity;
using WebApp.Services;
using System.Linq;
using WebApp.Services.SportService;
using WebApp.Services.PositionService;
using System.Collections;

namespace WebApp.Web.Controllers
{
    public class HomePageController : Controller
    {
        private readonly IEventService _eventService;
        private readonly UserManager<WebAppUser> _userManager;
        private readonly ImageService _imageService;
        private readonly ISportService _sportService;
        private readonly IPositionService _positionSerivce;

        public HomePageController(IEventService eventService, UserManager<WebAppUser> userManager, ISportService sportService, IPositionService positionService)
        {
            this._eventService = eventService;
            this._userManager = userManager;
            this._imageService = new ImageService();
            this._sportService = sportService;
            this._positionSerivce = positionService;
        }

        public IActionResult HomePageView()
        {

            return ReturnMainView();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult HomePageView(EventBindingModel model, IFormFile eventImage)
        {
            Event newEvent = new Event();
            MapModelToDB(model, eventImage, newEvent);

            this._eventService.CreateEvent(newEvent);
            return ReturnMainView();
        }

        private ViewResult ReturnMainView()
        {
            HomePageBinding model = new HomePageBinding();
            foreach (var dbEvent in _eventService.GetAllEvents())
            {
                EventBindingModel eventModel = new EventBindingModel();
                MapDbToModel(dbEvent, eventModel);
                model.Events.Add(eventModel);
            }

            return View(model);
        }

        [HttpGet]
        public JsonResult GetEvent(object model)
        {
            return Json(model);
        }

        private void MapModelToDB(EventBindingModel model, IFormFile eventImage, Event newEvent)
        {
            newEvent.Name = model.Title;
            newEvent.Time = model.Time;
            newEvent.SportId = model.SportId;
            newEvent.Description = model.Description;
            newEvent.Location = model.Location;
            if(eventImage == null)
            {
                newEvent.Image = "defaultImage.jpg";
            }
            else
            {
                newEvent.Image = this._imageService.UploadImage(eventImage);
            }
            newEvent.AdminId = this._userManager.GetUserId(User);
        }
        private void MapDbToModel(Event viewEvent, EventBindingModel model)
        {
            viewEvent.Sport = _sportService.GetSports().Where(s => s.Id == viewEvent.SportId).SingleOrDefault();
            viewEvent.Sport.Positions = _positionSerivce.GetPositions().Where(p => p.SportId == viewEvent.SportId).ToList();
            model.Id = viewEvent.Id;
            model.AdminId = viewEvent.AdminId;
            model.Title = viewEvent.Name;
            model.Time = viewEvent.Time;
            model.Location = viewEvent.Location;
            model.Description = viewEvent.Description;

            AttachPositions(viewEvent, model);
            if (viewEvent.Users.Count > 0)
            {
                AttachUsersToPositions(viewEvent, model);
            }
        }
        private static void AttachUsersToPositions(Event viewEvent, EventBindingModel model)
        {
            foreach (var position in model.Positions)
            {
                var positionUser = viewEvent.Users.Where(u => u.PositionId == position.Id).Single();
                position.Aprooved.Id = positionUser.User.Id;
                position.Aprooved.Name = positionUser.User.UserName;
                //position.Aprooved.Rating
                foreach (var toBeAprooved in viewEvent.Positions.Where(u => u.PositionId == position.Id))
                {
                    PlayerModel pending = new PlayerModel();
                    pending.Id = toBeAprooved.User.Id;
                    pending.Name = toBeAprooved.User.UserName;
                }
            }
        }
        private static void AttachPositions(Event viewEvent, EventBindingModel model)
        {
            foreach (var position in viewEvent.Sport.Positions)
            {
                PositionModel newPosition = new PositionModel();
                newPosition.Id = position.Id;
                newPosition.Name = position.Name;
                newPosition.Team = position.Team;
                model.Positions.Add(newPosition);
            }
        }
    }
}