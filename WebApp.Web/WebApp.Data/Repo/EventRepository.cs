namespace WebApp.Data.Repo
{
    using Domain;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class EventRepository : Repository<Event> ,IEventRepository
    {
        private readonly WebAppDbContext _context;
        public EventRepository(WebAppDbContext dbContext) :base(dbContext)
        {
        }

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

        public Event GetEvent(int id)
        {
            return (Event)_context.Events.Where(e => e.Id == id);
        }
    }
}
