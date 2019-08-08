using System;
using System.Collections.Generic;
using System.Text;
using WebApp.Domain;

namespace WebApp.Data.Repo
{
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
