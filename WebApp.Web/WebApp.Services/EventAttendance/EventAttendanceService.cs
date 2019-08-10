using System;
using System.Collections.Generic;
using System.Text;
using WebApp.Data.CustomRepos;
using WebApp.Data.Repo;

namespace WebApp.Services.EventAttendance
{
    public class EventAttendanceService : BaseService, IEventAttendanceService
    {
        private readonly IEventAttendeesRepo _eventAttendeesRepo;
        private readonly IEventAttendeesToBeApprovedRepo _eventAttendeesToBeApprovedRepo;

        public EventAttendanceService(IEventAttendeesToBeApprovedRepo eventAttendeesToBeApprovedRepo, IEventAttendeesRepo eventAttendeesRepo, IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            this._eventAttendeesRepo = eventAttendeesRepo;
            this._eventAttendeesToBeApprovedRepo = eventAttendeesToBeApprovedRepo;
        }
    }
}
