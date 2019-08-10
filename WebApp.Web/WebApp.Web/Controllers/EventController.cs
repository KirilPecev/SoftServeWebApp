using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebApp.Domain;
using WebApp.Services;
using WebApp.Services.EventService;
using WebApp.Services.PositionService;
using WebApp.Services.SportService;
using WebApp.Web.Models.Event;

namespace WebApp.Web.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventService _eventService;
        private readonly UserManager<WebAppUser> _userManager;
        private readonly ImageService _imageService;
        private readonly ISportService _sportService;
        private readonly IPositionService _positionSerivce;

        public EventController(IEventService eventService, UserManager<WebAppUser> userManager, ISportService sportService, IPositionService positionService)
        {
            this._eventService = eventService;
            this._userManager = userManager;
            this._imageService = new ImageService();
            this._sportService = sportService;
            this._positionSerivce = positionService;
        }
        public IActionResult ViewEvent(int Id)
        {
            Event viewEvent = this._eventService.GetEvent(Id);
            EventBindingModel model = new EventBindingModel();
            MapDbToModel(viewEvent, model);
            return View(model);
        }
        private void MapDbToModel(Event viewEvent, EventBindingModel model)
        {
            viewEvent.Sport = _sportService.GetSports().Where(s => s.Id == viewEvent.SportId).SingleOrDefault();
            viewEvent.Sport.Positions = _positionSerivce.GetPositions().Where(p => p.SportId == viewEvent.SportId).ToList();
            model.Id = viewEvent.Id;
            model.AdminId = viewEvent.AdminId;
            model.ImageURL = _imageService.GetImageURL(viewEvent.Image);
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