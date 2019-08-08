namespace WebApp.Data
{
    using Domain;
    using EntityTypeConfigurations;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class WebAppDbContext : IdentityDbContext<WebAppUser, IdentityRole, string>
    {
        public WebAppDbContext(DbContextOptions<WebAppDbContext> options)
            : base(options) { }

        public DbSet<Event> Events { get; set; }

        public DbSet<EventAttendees> EventAttendees { get; set; }

        public DbSet<EventAttendeesToBeApproved> EventAttendeesToBeApproved { get; set; }

        public DbSet<Position> Positions { get; set; }

        public DbSet<Rating> Ratings { get; set; }

        public DbSet<Sport> Sports { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new RatingConfiguration());
            base.OnModelCreating(builder);
        }
    }
}
