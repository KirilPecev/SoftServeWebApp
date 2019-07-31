using System.Threading.Tasks;

namespace WebApp.Data.Repo
{
    public interface IUnitOfWork
    {
        Task<int> SaveChanges();

    }
}
