namespace WebApp.Domain
{
    using System.Collections.Generic;

    public class Rating : BaseModel<int>
    {
        public Rating()
        {
            this.Scores = new List<Score>();
        }

        public string ReceiverId { get; set; }
        public virtual WebAppUser Receiver { get; set; }

        public string GiverId { get; set; }
        public virtual WebAppUser Giver { get; set; }

        public int EventId { get; set; }
        public virtual Event Event { get; set; }

        public ICollection<Score> Scores { get; set; }
    }
}
