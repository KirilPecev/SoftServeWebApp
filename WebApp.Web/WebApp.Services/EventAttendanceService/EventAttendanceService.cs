namespace WebApp.Services.EventAttendanceService
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
        private readonly IEventAttendeesRepository eventAttendees;
        private readonly IEventAttendeesToBeApprovedRepository eventAttendeesToBeApproved;

        public EventAttendanceService(
            IEmailSender sender,
            UserManager<WebAppUser> userManager,
            IEventAttendeesToBeApprovedRepository eventAttendeesToBeApproved,
            IEventAttendeesRepository eventAttendees,
            IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            this.sender = sender;
            this.userManager = userManager;
            this.eventAttendees = eventAttendees;
            this.eventAttendeesToBeApproved = eventAttendeesToBeApproved;
        }

        public async Task<EventAttendeesToBeApproved> RegisterUserForEvent(string userId, int eventId, int positionId)
        {
            var newEventAttendee = new EventAttendeesToBeApproved
            {
                UserId = userId,
                EventId = eventId,
                PositionId = positionId
            };

            var all = eventAttendeesToBeApproved.GetAll().ToList();

            if (!all.Exists(a => a.EventId == eventId && a.UserId == a.UserId && a.PositionId == positionId))
            {
                await eventAttendeesToBeApproved.AddAsync(newEventAttendee);
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
            await eventAttendees.AddAsync(approvedUser);
            await SaveChangesAsync();

            var email = this.userManager.Users.SingleOrDefault(u => u.Id == approvedUser.UserId).Email;
            await this.sender.SendEmailAsync(email, "Event", "You have been approved for event. Check you events.");

            return approvedUser;
        }

        public IEnumerable<EventAttendees> GetAllEventAttendeesForUser(string userId)
        {
            return eventAttendees.GetAllByUserId(userId);
        }

        public IEnumerable<EventAttendees> GetAllEventAttendeesForEvent(int eventId)
        {
            return eventAttendees.GetAll().Where(a => a.EventId == eventId).ToList();
        }

        public IEnumerable<EventAttendeesToBeApproved> GetAllEventAttendeesToBeApprovedForEvent(int eventId)
        {
            return eventAttendeesToBeApproved.GetAll().Where(a => a.EventId == eventId).ToList();
        }

        public IEnumerable<EventAttendeesToBeApproved> GetAllEventAttendeesToBeApprovedForUser(string userId)
        {
            return eventAttendeesToBeApproved.GetAllByUserId(userId);
        }

        public void RemoveUserAttendee(string userId, int eventId, int positionId)
        {
            eventAttendees.RemoveUser(userId, eventId, positionId);
        }

        public void RemoveUserAttendeeToBeApproved(string userId, int eventId, int positionId)
        {
            eventAttendeesToBeApproved.ClearUsers(userId, eventId);
        }
    }
}
