using System;
using System.Collections.Generic;
using System.Text;
using WebApp.Data.Repo;

namespace WebApp.Services.AdminEvent
{
    public class AdminEventService : BaseService, IAdminEventService
    {
        public AdminEventService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }
    }
}
