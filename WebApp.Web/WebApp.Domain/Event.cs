namespace WebApp.Domain
{
    using System;
    using System.Collections.Generic;

    public class Event : BaseModel<int>
    {
        public Event()
        {
            this.Users = new HashSet<EventAttendees>();
            this.Positions = new HashSet<EventAttendeesToBeApproved>();
            this.Ratings = new HashSet<Rating>();
        }

        public string AdminId { get; set; }
        public virtual WebAppUser Admin { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public int SportId { get; set; }
        public virtual Sport Sport { get; set; }

        public DateTime Time { get; set; }
        public string Location { get; set; }

        public string Description { get; set; }

        public string Location { get; set; }

        public string Description { get; set; }

        public virtual ICollection<EventAttendees> Users { get; set; }

        public virtual ICollection<EventAttendeesToBeApproved> Positions { get; set; }

        public virtual ICollection<Rating> Ratings { get; set; }
    }
}
