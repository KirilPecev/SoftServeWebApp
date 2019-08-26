namespace WebApp.Data.Repo.UnitOfWork
{
    using System.Threading.Tasks;

    public interface IUnitOfWork
    {
        Task<int> SaveChanges();
    }
}
