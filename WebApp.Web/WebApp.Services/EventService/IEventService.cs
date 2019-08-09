namespace WebApp.Services.EventService
{
    using System.Collections.Generic;
    using Domain;

    public interface IEventService
    {
        void CreateEvent(Event createEvent);

        void SaveEvent();

        IEnumerable<Event> GetAllEvents();
    }
}
