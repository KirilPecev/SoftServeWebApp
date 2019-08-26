namespace WebApp.Services.EventAttendance
{
    using Data.Repo.EventAttendeesRepo;
    using Data.Repo.EventAttendeesToBeApprovedRepo;
    using Data.Repo.UnitOfWork;
    using Domain;
    using Microsoft.AspNetCore.Identity;
    using Notifications;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class EventAttendanceService : BaseService, IEventAttendanceService
    {
        private readonly IEmailSender sender;
        private readonly UserManager<WebAppUser> userManager;
        private readonly IEventAttendeesRepo eventAttendeesRepo;
        private readonly IEventAttendeesToBeApprovedRepo eventAttendeesToBeApprovedRepo;

        public EventAttendanceService(IEmailSender sender, UserManager<WebAppUser> userManager, IEventAttendeesToBeApprovedRepo eventAttendeesToBeApprovedRepo, IEventAttendeesRepo eventAttendeesRepo, IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            this.sender = sender;
            this.userManager = userManager;
            this.eventAttendeesRepo = eventAttendeesRepo;
            this.eventAttendeesToBeApprovedRepo = eventAttendeesToBeApprovedRepo;
        }

        public async Task<EventAttendeesToBeApproved> RegisterUserForEvent(string userId, int eventId, int positionId)
        {
            var newEventAttendee = new EventAttendeesToBeApproved
            {
                UserId = userId,
                EventId = eventId,
                PositionId = positionId
            };

            var all = eventAttendeesToBeApprovedRepo.GetAll().ToList();

            if (!all.Exists(a => a.EventId == eventId && a.UserId == a.UserId && a.PositionId == positionId))
            {
                await eventAttendeesToBeApprovedRepo.AddAsync(newEventAttendee);
                await SaveChangesAsync();
            }

            return newEventAttendee;
        }

        public async Task<EventAttendees> ApproveUserForeEvent(string userId, int eventId, int positionId)
        {
            var approvedUser = new EventAttendees
            {
                UserId = userId,
                EventId = eventId,
                PositionId = positionId
            };

            RemoveUserAttendeeToBeApproved(userId, eventId, positionId);
            await eventAttendeesRepo.AddAsync(approvedUser);
            await SaveChangesAsync();

            var email = this.userManager.Users.SingleOrDefault(u => u.Id == approvedUser.UserId).Email;
            await this.sender.SendEmailAsync(email, "Event", "You have been approved for event. Check you events.");

            return approvedUser;
        }

        public IEnumerable<EventAttendees> GetAllEventAttendeesForUser(string userId)
        {
            return eventAttendeesRepo.GetAllByUserId(userId);
        }

        public IEnumerable<EventAttendees> GetAllEventAttendeesForEvent(int eventId)
        {
            return eventAttendeesRepo.GetAll().Where(a => a.EventId == eventId).ToList();
        }

        public IEnumerable<EventAttendeesToBeApproved> GetAllEventAttendeesToBeApprovedForEvent(int eventId)
        {
            return eventAttendeesToBeApprovedRepo.GetAll().Where(a => a.EventId == eventId).ToList();
        }

        public IEnumerable<EventAttendeesToBeApproved> GetAllEventAttendeesToBeApprovedForUser(string userId)
        {
            return eventAttendeesToBeApprovedRepo.GetAllByUserId(userId);
        }

        public void RemoveUserAttendee(string userId, int eventId, int positionId)
        {
            eventAttendeesRepo.RemoveUser(userId, eventId, positionId);
        }

        public void RemoveUserAttendeeToBeApproved(string userId, int eventId, int positionId)
        {
            eventAttendeesToBeApprovedRepo.ClearUsers(userId, eventId);
        }
    }
}
