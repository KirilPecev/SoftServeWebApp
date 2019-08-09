namespace WebApp.Services.EventService
{
    using Domain;

    public interface IEventService
    {
        void CreateEvent(Event createEvent);

        void SaveEvent();

        IEnumerable<Event> GetAllEvents();
    }
}
