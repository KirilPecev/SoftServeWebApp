namespace WebApp.Services.EventService
{
    using Domain;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IEventService
    {
        void CreateEvent(Event createEvent);

        IEnumerable<Event> GetAllEvents();

        Event GetEvent(int id);

        IEnumerable<Event> GetAllEventsByUser(string id);

        Task DeleteEvent(int id);

        void EditEvent(Event editEvent);
    }
}
