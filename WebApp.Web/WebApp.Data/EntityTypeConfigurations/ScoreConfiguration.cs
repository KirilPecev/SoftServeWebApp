namespace WebApp.Data.EntityTypeConfigurations
{
    using Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ScoreConfiguration : IEntityTypeConfiguration<Score>
    {
        public void Configure(EntityTypeBuilder<Score> builder)
        {
            builder
                .HasOne(score => score.Rating)
                .WithMany(rating => rating.Scores )
                .HasForeignKey(fk => fk.RatingId);
        }
    }
}
