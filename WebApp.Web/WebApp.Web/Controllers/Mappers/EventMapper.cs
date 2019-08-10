using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebApp.Domain;
using WebApp.Services;
using WebApp.Services.EventService;
using WebApp.Services.PositionService;
using WebApp.Services.SportService;
using WebApp.Web.Models.Event;

namespace WebApp.Web.Controllers.Mappers
{
    public class EventMapper : Controller, IEventMapper
    {
        private readonly UserManager<WebAppUser> _userManager;
        private readonly ImageService _imageService;
        private readonly ISportService _sportService;
        private readonly IPositionService _positionSerivce;
        public EventMapper(IEventService eventService, UserManager<WebAppUser> userManager, ISportService sportService, IPositionService positionService)
        {
            this._userManager = userManager;
            this._imageService = new ImageService();
            this._sportService = sportService;
            this._positionSerivce = positionService;
        }
        public Event MapEventToDB(EventBindingModel model, IFormFile eventImage)
        {
            Event newEvent = new Event();
            newEvent.Name = model.Title;
            newEvent.Time = model.Time;
            newEvent.SportId = model.SportId;
            newEvent.Description = model.Description;
            newEvent.Location = model.Location;
            if (eventImage == null)
            {
                newEvent.Image = "defaultImage.jpg";
            }
            else
            {
                newEvent.Image = this._imageService.UploadImage(eventImage);
            }
            newEvent.AdminId = this._userManager.GetUserId(User);

            return newEvent;
        }
        public EventBindingModel MapDbToEvent(Event dbEvent)
        {
            EventBindingModel model = new EventBindingModel();
            dbEvent.Sport = _sportService.GetSports().Where(s => s.Id == dbEvent.SportId).SingleOrDefault();
            dbEvent.Sport.Positions = _positionSerivce.GetPositions().Where(p => p.SportId == dbEvent.SportId).ToList();
            model.Id = dbEvent.Id;
            model.AdminId = dbEvent.AdminId;
            model.Title = dbEvent.Name;
            model.Time = dbEvent.Time;
            model.Location = dbEvent.Location;
            model.Description = dbEvent.Description;
            model.ImageURL = _imageService.GetImageURL(dbEvent.Image);

            AttachPositions(dbEvent, model);
            if (dbEvent.Users.Count > 0)
            {
                AttachUsersToPositions(dbEvent, model);
            }
            return model;
        }
        private static void AttachUsersToPositions(Event dbEvent, EventBindingModel model)
        {
            foreach (var position in model.Positions)
            {
                var positionUser = dbEvent.Users.Where(u => u.PositionId == position.Id).Single();
                position.Aprooved.Id = positionUser.User.Id;
                position.Aprooved.Name = positionUser.User.UserName;
                //position.Aprooved.Rating
                foreach (var toBeAprooved in dbEvent.Positions.Where(u => u.PositionId == position.Id))
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
