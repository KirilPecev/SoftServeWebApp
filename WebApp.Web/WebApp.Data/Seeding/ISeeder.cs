namespace WebApp.Data.Seeding
{
    using System.Threading.Tasks;

    public interface ISeeder
    {
        Task SeedAsync(WebAppDbContext dbContext);
    }
}

