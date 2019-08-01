namespace WebApp.Data.Seeding
{
    using Domain;
    using Microsoft.EntityFrameworkCore.Internal;
    using System.Linq;
    using System.Threading.Tasks;

    public class SportsSeeder : ISeeder
    {
        public async Task SeedAsync(WebAppDbContext dbContext)
        {
            string[] positions = new[]
            {
                "Goalkeeper",
                "Right Fullback",
                "Left Fullback",
                "Center Back",
                "Center Back",
                "Defending Midfielder",
                "Right Midfielder",
                "Central Midfielder",
                "Striker",
                "Attacking Midfielder",
                "Left Midfielder"
            };

            await SeedRoleAsync(dbContext, "Football", positions);
        }

        private static async Task SeedRoleAsync(WebAppDbContext dbContext, string sportName, string[] positions)
        {
            if (!dbContext.Sports.Any(s => s.Name == sportName))
            {
                Sport sport = new Sport()
                {
                    Name = sportName
                };

                foreach (var position in positions)
                {
                    if (!sport.Positions.Any(p => p.Name == position))
                    {
                        sport.Positions.Add(new Position()
                        {
                            Name = position,
                        });
                    }
                }

                await dbContext.Sports.AddAsync(sport);
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
