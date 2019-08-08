using System;
using System.Collections.Generic;
using System.Text;
using WebApp.Data.Repo;

namespace WebApp.Services.AdminEvent
{
    public class EventAttendanceService : BaseService, IAdminEventService
    {
        public EventAttendanceService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }
    }
}
