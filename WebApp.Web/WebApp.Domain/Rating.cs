namespace WebApp.Domain
{
    public class Rating : BaseModel<int>
    {
        public string ReceiverId { get; set; }
        public virtual WebAppUser Receiver { get; set; }

        public string GiverId { get; set; }
        public virtual WebAppUser Giver { get; set; }

        public int EventId { get; set; }
        public virtual Event Event { get; set; }

        public int Score { get; set; }
    }
}
