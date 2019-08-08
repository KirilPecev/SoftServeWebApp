using System;
using System.Collections.Generic;
using System.Text;
using WebApp.Data.CustomRepos;
using WebApp.Data.Repo;

namespace WebApp.Services.EventAttendance
{
    public class EventAttendanceService : BaseService, IEventAttendanceService
    {
        private readonly IEventAttendanceService _eventAttendanceService;
        private readonly IEventAttendeesToBeApprovedRepo _eventAttendeesToBeApprovedRepo;

        public EventAttendanceService(IEventAttendeesToBeApprovedRepo eventAttendeesToBeApprovedRepo, IEventAttendanceService eventAttendanceService, IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            this._eventAttendanceService = eventAttendanceService;
            this._eventAttendeesToBeApprovedRepo = eventAttendeesToBeApprovedRepo;
        }
    }
}
