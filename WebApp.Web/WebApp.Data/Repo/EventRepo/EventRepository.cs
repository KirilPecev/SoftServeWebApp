namespace WebApp.Data.Repo.EventRepo
{
    using Domain;
    using GenericRepository;
    using System.Collections.Generic;
    using System.Linq;

    public class EventRepository : Repository<Event>, IEventRepository
    {
        private readonly WebAppDbContext dbContext;

        public EventRepository(WebAppDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public void CreateEvent(Event createEvent)
        {
            dbSet.Add(createEvent);
            dbContext.SaveChanges();
        }

        public IEnumerable<Event> GetAllEvents()
        {
            return dbSet;
        }

        public Event GetEvent(int id)
        {
            return dbSet.SingleOrDefault(e => e.Id == id);
        }
    }
}
