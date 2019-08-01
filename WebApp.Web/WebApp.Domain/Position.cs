namespace WebApp.Domain
{
    using System.Collections.Generic;

    public class Position : BaseModel<int>
    {
        public Position()
        {
            this.EventAttendees = new List<EventAttendees>();
            this.EventAttendeesToBeApproved = new List<EventAttendeesToBeApproved>();
        }

        public string Name { get; set; }

        public byte Team { get; set; }

        public int SportId { get; set; }
        public virtual Sport Sport { get; set; }

        public virtual ICollection<EventAttendees> EventAttendees { get; set; }

        public virtual ICollection<EventAttendeesToBeApproved> EventAttendeesToBeApproved { get; set; }
    }
}
