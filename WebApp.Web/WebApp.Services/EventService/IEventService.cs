namespace WebApp.Services.EventService
{
    using System.Collections.Generic;
    using Domain;
    using System.Collections.Generic;

    public interface IEventService
    {
        void CreateEvent(Event createEvent);

        void SaveEvent();

        IEnumerable<Event> GetAllEvents();

        Event GetEvent(int id);
    }
}
