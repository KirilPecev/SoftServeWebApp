namespace WebApp.Data.EntityTypeConfigurations
{
    using Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class RatingConfiguration : IEntityTypeConfiguration<Rating>
    {
        public void Configure(EntityTypeBuilder<Rating> builder)
        {
            builder.HasOne(rating => rating.User)
                .WithOne(user => user.Rating)
                .HasForeignKey<Rating>(key => key.UserId);
        }
    }
}
