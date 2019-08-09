namespace WebApp.Data.Repo
{
    using Domain;
    using System.Collections.Generic;

    public class EventRepository : IEventRepository
    {
        private readonly WebAppDbContext _context;

        public void CreateEvent(Event createEvent)
        {
            _context.Events.Add(createEvent);
        }

        public void SaveEvent()
        {
            _context.SaveChanges();
        }

        public IEnumerable<Event> GetAllEvents()
        {
            return _context.Events;
        }
    }
}
