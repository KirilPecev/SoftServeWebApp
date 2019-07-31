namespace WebApp.Data.EntityTypeConfigurations
{
    using Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class EventAttendeesConfiguration : IEntityTypeConfiguration<EventAttendees>
    {
        public void Configure(EntityTypeBuilder<EventAttendees> builder)
        {
            builder
                .HasKey(key => new { key.UserId, key.EventId });

            builder
                .HasOne(rl => rl.User)
                .WithMany(user => user.Events)
                .HasForeignKey(rl => rl.UserId);

            builder
                .HasOne(rl => rl.Event)
                .WithMany(e => e.Users)
                .HasForeignKey(rl => rl.EventId);
        }
    }
}
