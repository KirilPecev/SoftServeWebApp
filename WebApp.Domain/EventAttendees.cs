namespace WebApp.Domain
{
    public class EventAttendees : BaseModel<int>
    {
        public string UserId { get; set; }
        public virtual WebAppUser User { get; set; }

        public int EventId { get; set; }
        public virtual Event Event { get; set; }

        public int PositionId { get; set; }
        public virtual Position Position { get; set; }
    }
}
