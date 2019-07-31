namespace WebApp.Data.EntityTypeConfigurations
{
    using Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class RankListConfiguration : IEntityTypeConfiguration<RankList>
    {
        public void Configure(EntityTypeBuilder<RankList> builder)
        {
            builder
                .HasKey(key => new { key.UserId, key.SportId });

            builder
                .HasOne(rl => rl.User)
                .WithMany(user => user.RankLists)
                .HasForeignKey(rl => rl.UserId);

            builder
                .HasOne(rl => rl.Sport)
                .WithMany(sport => sport.RankLists)
                .HasForeignKey(rl => rl.SportId);
        }
    }
}
