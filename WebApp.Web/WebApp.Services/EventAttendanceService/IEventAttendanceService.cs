namespace WebApp.Services.EventAttendanceService
{
    using Domain;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IEventAttendanceService
    {
        Task<EventAttendeesToBeApproved> RegisterUserForEvent(string userId, int eventId, int positionId);

        Task<EventAttendees> ApproveUserForeEvent(string userId, int eventId, int positionId);

        IEnumerable<EventAttendeesToBeApproved> GetAllEventAttendeesToBeApprovedForUser(string userId);

        IEnumerable<EventAttendees> GetAllEventAttendeesForUser(string userId);

        IEnumerable<EventAttendeesToBeApproved> GetAllEventAttendeesToBeApprovedForEvent(int eventId);

        IEnumerable<EventAttendees> GetAllEventAttendeesForEvent(int eventId);

        void RemoveUserAttendee(string userId, int eventId, int positionId);

        void RemoveUserAttendeeToBeApproved(string userId, int eventId, int positionId);
    }
}
