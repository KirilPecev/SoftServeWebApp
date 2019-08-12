using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Domain;

namespace WebApp.Services.EventAttendance
{
    public interface IEventAttendanceService
    {

        Task<EventAttendeesToBeApproved> RegisterUserForEvent(string userId, int eventId, int positionId);
        Task<EventAttendees> ApproveUserForeEvent(string userId, int eventId, int positionId);
        IEnumerable<EventAttendeesToBeApproved> GetAllEventAttendeesToBeApprovedForUser(string userId);
        IEnumerable<EventAttendees> GetAllEventAttendeesForUser(string userId);
        List<EventAttendeesToBeApproved> GetAllEventAttendeesToBeApprovedForEvent(int eventId);
        List<EventAttendees> GetAllEventAttendeesForEvent(int eventId);
        void RemoveUserAtendee(string userId,int  eventId,int positionId);
        void RemoveUserAtendeeToBeAprooved(string userId, int eventId, int positionId);

    }
}
