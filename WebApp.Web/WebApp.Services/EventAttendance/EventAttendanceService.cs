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
    public class EventAttendanceService : BaseService, IEventAttendanceService
    {
        private readonly IEventAttendeesRepo _eventAttendeesRepo;
        private readonly IEventAttendeesToBeApprovedRepo _eventAttendeesToBeApprovedRepo;

        public EventAttendanceService(IEventAttendeesToBeApprovedRepo eventAttendeesToBeApprovedRepo, IEventAttendeesRepo eventAttendeesRepo, IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
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

            await _eventAttendeesRepo.AddAsync(approvedUser);
            RemoveUserAtendeeToBeAprooved(userId, eventId, positionId);
            await SaveChangesAsync();
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
            _eventAttendeesToBeApprovedRepo.RemoveUser(userId, eventId, positionId);
        }
    }
}
