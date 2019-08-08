using System;
using System.Collections.Generic;
using System.Text;
using WebApp.Data.Repo;
using WebApp.Domain;

namespace WebApp.Services.EventAttendance
{
    public interface IEventAttendanceService : IRepository<EventAttendeesToBeApproved>
    {
    }
}
