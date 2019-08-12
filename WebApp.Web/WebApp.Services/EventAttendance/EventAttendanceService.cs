using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Data.CustomRepos;
using WebApp.Data.Repo;
using WebApp.Domain;

namespace WebApp.Services.EventAttendance
{
    using Microsoft.AspNetCore.Identity;
    using Notifications;

    public class EventAttendanceService : BaseService, IEventAttendanceService
    {
        private readonly IEmailSender sender;
        private readonly UserManager<WebAppUser> userManager;
        private readonly IEventAttendeesRepo _eventAttendeesRepo;
        private readonly IEventAttendeesToBeApprovedRepo _eventAttendeesToBeApprovedRepo;

        public EventAttendanceService(IEmailSender sender, UserManager<WebAppUser> userManager, IEventAttendeesToBeApprovedRepo eventAttendeesToBeApprovedRepo, IEventAttendeesRepo eventAttendeesRepo, IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            this.sender = sender;
            this.userManager = userManager;
            this._eventAttendeesRepo = eventAttendeesRepo;
            this._eventAttendeesToBeApprovedRepo = eventAttendeesToBeApprovedRepo;
        }

        public async Task<EventAttendeesToBeApproved> RegisterUserForEvent(string userId, int eventId, int positionId)
        {
            var newEventAttendee = new EventAttendeesToBeApproved
            {
                UserId = userId,
                EventId = eventId,
                PositionId = positionId
            };

            await _eventAttendeesToBeApprovedRepo.AddAsync(newEventAttendee);
            await SaveChangesAsync();

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

            RemoveUserAtendeeToBeAprooved(userId, eventId, positionId);
            await _eventAttendeesRepo.AddAsync(approvedUser);
            await SaveChangesAsync();

            var email = this.userManager.Users.SingleOrDefault(u => u.Id == approvedUser.UserId).Email;
            await this.sender.SendEmailAsync(email, "Event", "You have been approved for event. Check you events.");

            return approvedUser;
        }

        public IEnumerable<EventAttendees> GetAllEventAttendeesForUser(string userId)
        {
            var allAttendeesForUser = _eventAttendeesRepo.GetAllByUserId(userId);

            return allAttendeesForUser;
        }

        public List<EventAttendees> GetAllEventAttendeesForEvent(int eventId)
        {
            var allAttendeesForEvent = _eventAttendeesRepo.GetAll().Where(a => a.EventId == eventId).ToList();

            return allAttendeesForEvent;
        }

        public List<EventAttendeesToBeApproved> GetAllEventAttendeesToBeApprovedForEvent(int eventId)
        {
            var allAttendeesForEvent = _eventAttendeesToBeApprovedRepo.GetAll().Where(a => a.EventId == eventId).ToList();
            return allAttendeesForEvent;
        }

        public IEnumerable<EventAttendeesToBeApproved> GetAllEventAttendeesToBeApprovedForUser(string userId)
        {

            var allAttendeesForUser = _eventAttendeesToBeApprovedRepo.GetAllByUserId(userId);
            return allAttendeesForUser;
        }
        public void RemoveUserAtendee(string userId, int eventId, int positionId)
        {
            _eventAttendeesRepo.RemoveUser(userId, eventId, positionId);
        }
        public void RemoveUserAtendeeToBeAprooved(string userId, int eventId, int positionId)
        {
            _eventAttendeesToBeApprovedRepo.ClearUsers(userId, eventId);
        }
    }
}
