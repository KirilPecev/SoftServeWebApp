using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebApp.Domain;
using WebApp.Services;
using WebApp.Services.EventAttendance;
using WebApp.Services.EventService;
using WebApp.Services.PositionService;
using WebApp.Services.SportService;
using WebApp.Web.Models.Event;

namespace WebApp.Web.Controllers.Mappers
{
    public class EventMapper : Controller, IEventMapper
    {
        private readonly UserManager<WebAppUser> userManager;
        private readonly ImageService _imageService;
        private readonly ISportService _sportService;
        private readonly IPositionService _positionSerivce;
        private readonly IEventAttendanceService _attendanceService;
        public EventMapper(IEventService eventService, ISportService sportService, IPositionService positionService, 
            IEventAttendanceService attendanceService, UserManager<WebAppUser> userManager)
        {
            this._imageService = new ImageService();
            this._sportService = sportService;
            this._positionSerivce = positionService;
            this._attendanceService = attendanceService;
            this.userManager = userManager;
        }
        public Event MapEventToDB(EventBindingModel model, IFormFile eventImage, string adminId)
        {
            Event newEvent = new Event();
            newEvent.Id = model.Id;
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
            newEvent.AdminId = adminId;

            return newEvent;
        }

        public Event MapEditEventToDB(EventBindingModel model, IFormFile eventImage, string adminId)
        {
            Event newEvent = new Event();
            newEvent.Id = model.Id;
            newEvent.Name = model.Title;
            newEvent.Time = model.Time;
            newEvent.SportId = model.SportId;
            newEvent.Description = model.Description;
            newEvent.Location = model.Location;
            if (eventImage != null)
            {
                newEvent.Image = this._imageService.UploadImage(eventImage);
            }
            newEvent.AdminId = adminId;

            return newEvent;
        }

        public EventBindingModel MapDbToEvent(Event dbEvent)
        {
            EventBindingModel model = new EventBindingModel();
            dbEvent.Sport = _sportService.GetSports().Where(s => s.Id == dbEvent.SportId).SingleOrDefault();
            dbEvent.Sport.Positions = _positionSerivce.GetPositions().Where(p => p.SportId == dbEvent.SportId).ToList();
            AddUsersToPositions(dbEvent);
            model.Id = dbEvent.Id;
            model.AdminId = dbEvent.AdminId;
            model.Title = dbEvent.Name;
            model.Time = dbEvent.Time;
            model.Location = dbEvent.Location;
            model.Description = dbEvent.Description;
            model.ImageURL = _imageService.GetImageURL(dbEvent.Image);
            AttachPositions(dbEvent, model);
            AttachUsersToPositions(dbEvent, model);
            return model;
        }
        private void AddUsersToPositions(Event dbEvent)
        {
            if (dbEvent.Positions.Count == 0)
            {
                foreach (var position in dbEvent.Sport.Positions)
                {
                    var newPos = new EventAttendeesToBeApproved();
                    newPos.Position = new Position();
                    newPos.PositionId = position.Id;
                    newPos.Position.Id = position.Id;
                    newPos.Position.Name = position.Name;
                    dbEvent.Positions.Add(newPos);
                }
            }
            var atendees = _attendanceService.GetAllEventAttendeesForEvent(dbEvent.Id).ToList();
            foreach (var atendee in atendees)
            {
                var position = dbEvent.Positions.First(p => p.PositionId == atendee.PositionId);
                position.Position.EventAttendees.Add(atendee);
            }
            var atendeesForAprooving = _attendanceService.GetAllEventAttendeesToBeApprovedForEvent(dbEvent.Id).ToList();
            foreach (var atendee in atendeesForAprooving)
            {
                var position = dbEvent.Positions.First(p => p.PositionId == atendee.PositionId);
                position.Position.EventAttendeesToBeApproved.Add(atendee);
            }
        }
        private void AttachUsersToPositions(Event dbEvent, EventBindingModel model)
        {
            foreach (var position in model.Positions)
            {
                var dbPosition = dbEvent.Positions.FirstOrDefault(p => p.PositionId == position.Id);
                if(dbPosition != null && dbPosition.Position.EventAttendees.Count!=0)
                {
                    string aproovedPlayerId = dbPosition.Position.EventAttendees.First().UserId;
                    position.Aprooved.Name = userManager.Users.FirstOrDefault(u => u.Id == aproovedPlayerId).UserName;
                    position.Aprooved.Id = aproovedPlayerId;
                    position.Aprooved.EventId = dbEvent.Id;
                    position.Aprooved.PositionId = position.Id;
                }
                if (dbPosition != null && dbPosition.Position.EventAttendeesToBeApproved.Count != 0)
                {
                    var toBeAprooved = dbPosition.Position.EventAttendeesToBeApproved.ToList();
                    foreach (var user in toBeAprooved)
                    {
                        PlayerModel pendingAproval = new PlayerModel();
                        pendingAproval.Name = userManager.Users.FirstOrDefault(u => u.Id == user.UserId).NormalizedUserName;
                        pendingAproval.Id = user.UserId;
                        pendingAproval.EventId = dbEvent.Id;
                        pendingAproval.PositionId = position.Id;
                        position.ToBeAprooved.Add(pendingAproval);
                    }
                }
            }
        }
        private static void AttachPositions(Event viewEvent, EventBindingModel model)
        {
            foreach (var position in viewEvent.Sport.Positions)
            {
                PositionModel newPosition = new PositionModel();
                newPosition.EventId = viewEvent.Id;
                newPosition.Id = position.Id;
                newPosition.Name = position.Name;
                newPosition.Team = position.Team;
                model.Positions.Add(newPosition);
            }
        }
    }
}
