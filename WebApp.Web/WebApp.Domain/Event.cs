namespace WebApp.Domain
{
    using System.Collections.Generic;

    public class Event : BaseModel<int>
    {
        public Event()
        {
            this.Users = new HashSet<EventAttendees>();
        }

        public string Name { get; set; }

        public int SportId { get; set; }
        public virtual Sport Sport { get; set; }

        public virtual ICollection<EventAttendees> Users { get; set; }
    }
}
