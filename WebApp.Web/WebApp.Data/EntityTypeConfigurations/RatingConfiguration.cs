namespace WebApp.Data.EntityTypeConfigurations
{
    using Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class RatingConfiguration : IEntityTypeConfiguration<Rating>
    {
        public void Configure(EntityTypeBuilder<Rating> builder)
        {
            builder
                .HasOne(rating => rating.Receiver)
                .WithMany(user => user.Ratings)
                .HasForeignKey(fk => fk.ReceiverId);

        }
    }
}
