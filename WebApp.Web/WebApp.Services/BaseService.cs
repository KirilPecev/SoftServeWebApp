namespace WebApp.Services
{
    using Data.Repo.UnitOfWork;
    using System.Threading.Tasks;

    public class BaseService
    {
        readonly IUnitOfWork unitOfWork;

        public BaseService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await unitOfWork.SaveChanges();
        }
    }
}
