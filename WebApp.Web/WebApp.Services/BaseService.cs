using System.Threading.Tasks;
using WebApp.Data.Repo;

namespace WebApp.Services
{
    public class BaseService
    {
        readonly IUnitOfWork _unitOfWork;

        public BaseService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _unitOfWork.SaveChanges();
        }
    }
}
