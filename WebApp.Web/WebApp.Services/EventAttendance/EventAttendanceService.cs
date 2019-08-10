using System.Collections.Generic;
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
            await SaveChangesAsync();

            return approvedUser;
        }

        public IEnumerable<EventAttendees> GetAllEventAttendeesForUser(string userId)
        {
            var allAttendeesForUser = _eventAttendeesRepo.GetAllByUserId(userId);

            return allAttendeesForUser;
        }

        public IEnumerable<EventAttendeesToBeApproved> GetAllEventAttendeesToBeApprovedForUser(string userId)
        {
            var allAttendeesToBeApproved = _eventAttendeesToBeApprovedRepo.GetAllByUserId(userId);

            return allAttendeesToBeApproved;
        }
    }
}
