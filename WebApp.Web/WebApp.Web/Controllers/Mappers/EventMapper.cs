namespace WebApp.Web.Controllers.Mappers
{
    using Domain;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Models.Event;
    using Services;
    using Services.EventAttendanceService;
    using Services.EventService;
    using Services.PositionService;
    using Services.SportService;
    using System.Linq;

    public class EventMapper : IEventMapper
    {
        private readonly UserManager<WebAppUser> userManager;
        private readonly ImageService imageService;
        private readonly ISportService sportService;
        private readonly IPositionService positionService;
        private readonly IEventAttendanceService attendanceService;

        public EventMapper(IEventService eventService, ISportService sportService, IPositionService positionService,
            IEventAttendanceService attendanceService, UserManager<WebAppUser> userManager)
        {
            this.imageService = new ImageService();
            this.sportService = sportService;
            this.positionService = positionService;
            this.attendanceService = attendanceService;
            this.userManager = userManager;
        }

        public Event NewEvent(EventBindingModel model, IFormFile eventImage, string adminId)
        {
            Event newEvent = new Event();
            newEvent.Id = model.Id;
            newEvent.Name = model.Title;
            newEvent.Time = model.Time;
            newEvent.SportId = model.SportId;
            newEvent.Description = model.Description;
            newEvent.Location = model.Location;
            newEvent.Image = eventImage == null ? "defaultImage.jpg" : this.imageService.UploadImage(eventImage);
            newEvent.AdminId = adminId;

            return newEvent;
        }

        public Event ModifiedEvent(EventBindingModel model, IFormFile eventImage, string adminId)
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
                newEvent.Image = this.imageService.UploadImage(eventImage);
            }
            newEvent.AdminId = adminId;

            return newEvent;
        }

        public EventBindingModel ViewEvent(Event dbEvent)
        {
            EventBindingModel model = new EventBindingModel();
            dbEvent.Sport = sportService.GetSports().SingleOrDefault(s => s.Id == dbEvent.SportId);
            dbEvent.Sport.Positions = positionService.GetPositions().Where(p => p.SportId == dbEvent.SportId).ToList();
            AddUsersToPositions(dbEvent);
            model.Id = dbEvent.Id;
            model.AdminId = dbEvent.AdminId;
            model.AdminName = userManager.Users.First(u => u.Id == dbEvent.AdminId).UserName;
            model.Title = dbEvent.Name;
            model.Time = dbEvent.Time;
            model.Location = dbEvent.Location;
            model.Description = dbEvent.Description;
            model.ImageURL = imageService.GetImageUrl(dbEvent.Image);
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
            var atendees = attendanceService.GetAllEventAttendeesForEvent(dbEvent.Id).ToList();
            foreach (var atendee in atendees)
            {
                var position = dbEvent.Positions.First(p => p.PositionId == atendee.PositionId);
                position.Position.EventAttendees.Add(atendee);
            }
            var atendeesForAprooving = attendanceService.GetAllEventAttendeesToBeApprovedForEvent(dbEvent.Id).ToList();
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
                if (dbPosition != null && dbPosition.Position.EventAttendees.Count != 0)
                {
                    string aproovedPlayerId = dbPosition.Position.EventAttendees.First().UserId;
                    position.Approved.Name = userManager.Users.FirstOrDefault(u => u.Id == aproovedPlayerId).UserName;
                    position.Approved.Id = aproovedPlayerId;
                    position.Approved.EventId = dbEvent.Id;
                    position.Approved.PositionId = position.Id;
                }
                if (dbPosition != null && dbPosition.Position.EventAttendeesToBeApproved.Count != 0)
                {
                    var toBeAprooved = dbPosition.Position.EventAttendeesToBeApproved.ToList();
                    foreach (var user in toBeAprooved)
                    {
                        PlayerModel pendingAproval = new PlayerModel();
                        pendingAproval.Name = userManager.Users.FirstOrDefault(u => u.Id == user.UserId).UserName;
                        pendingAproval.Id = user.UserId;
                        pendingAproval.EventId = dbEvent.Id;
                        pendingAproval.PositionId = position.Id;
                        position.ToBeApproved.Add(pendingAproval);
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
