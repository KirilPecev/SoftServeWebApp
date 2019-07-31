namespace WebApp.Domain
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;

    public class WebAppUser : IdentityUser<string>
    {
        public WebAppUser()
        {
            this.Sports = new HashSet<Sport>();
            this.Events = new HashSet<EventAttendees>();
            this.RankLists = new HashSet<RankList>();
        }

        public string ImageUrl { get; set; }

        public int RatingId { get; set; }
        public virtual Rating Rating { get; set; }

        public string City { get; set; }

        public virtual ICollection<Sport> Sports { get; set; }

        public virtual ICollection<EventAttendees> Events { get; set; }

        public virtual ICollection<RankList> RankLists { get; set; }
    }
}
