namespace WebApp.Domain
{
    using System.Collections.Generic;

    public class Sport : BaseModel<int>
    {
        public Sport()
        {
            this.Events = new List<Event>();
            this.Positions = new HashSet<Position>();
        }

        public string Name { get; set; }

        public virtual ICollection<Event> Events { get; set; }

        public virtual ICollection<Position> Positions { get; set; }
    }
}
