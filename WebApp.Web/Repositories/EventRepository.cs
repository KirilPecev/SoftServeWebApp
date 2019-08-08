using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Data;
using WebApp.Domain;

namespace WebApp.Web.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly WebAppDbContext _context;
        private bool disposed = false;

        public void CreateEvent(Event createEvent)
        {
            _context.Events.Add(createEvent);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }

            this.disposed = true;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
