using System;
using System.Collections.Generic;
using System.Text;
using WebApp.Domain;

namespace WebApp.Data.Repo
{
    class EventRepo : IEventRepo
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

        public void SaveEvent()
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
