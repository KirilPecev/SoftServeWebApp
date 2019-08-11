namespace WebApp.Domain
{
    using System;

    public class Score : BaseModel<int>
    {
        public int RatingId { get; set; }
        public Rating Rating { get; set; }

        public DateTime DateTime { get; set; }

        public int Points { get; set; }
    }
}
