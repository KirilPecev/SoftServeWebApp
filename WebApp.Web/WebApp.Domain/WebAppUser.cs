namespace WebApp.Domain
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;

    public class WebAppUser : IdentityUser<string>
    {
        public WebAppUser()
        {
            this.EventAttendees = new List<EventAttendees>();
            this.EventAttendeesToBeApproved = new List<EventAttendeesToBeApproved>();
            this.Ratings = new List<Rating>();
        }

        public string Image { get; set; }

        public string City { get; set; }

        public virtual ICollection<Rating> Ratings { get; set; }

        public virtual ICollection<EventAttendees> EventAttendees { get; set; }

        public virtual ICollection<EventAttendeesToBeApproved> EventAttendeesToBeApproved { get; set; }
    }
}
