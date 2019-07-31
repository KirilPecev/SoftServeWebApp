namespace WebApp.Domain
{
    public class EventAttendees
    {
        public string UserId { get; set; }
        public virtual WebAppUser User { get; set; }

        public int EventId { get; set; }
        public virtual Event Event { get; set; }
    }
}
