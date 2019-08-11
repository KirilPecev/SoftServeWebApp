namespace WebApp.Services.EventService
{
    using Domain;
    using System.Collections.Generic;

    public interface IEventService
    {
        void CreateEvent(Event createEvent);

        IEnumerable<Event> GetAllEvents();

        Event GetEvent(int id);

        IEnumerable<Event> GetAllEventsByUser(string id);
    }
}
