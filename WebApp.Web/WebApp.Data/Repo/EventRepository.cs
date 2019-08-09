namespace WebApp.Data.Repo
{
    using Domain;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class EventRepository : Repository<Event> ,IEventRepository
    {
        public EventRepository(WebAppDbContext dbContext) :base(dbContext)
        {
        }

        public void CreateEvent(Event createEvent)
        {
            dbSet.Add(createEvent);
        }

        public IEnumerable<Event> GetAllEvents()
        {
            return dbSet;
        }

        public Event GetEvent(int id)
        {
            return dbSet.Find(id);
        }
    }
}
