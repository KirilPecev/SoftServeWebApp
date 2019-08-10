using System;
using System.Collections.Generic;
using System.Text;
using WebApp.Domain;

namespace WebApp.Services.SportService
{
    public interface ISportService
    {
        IEnumerable<Sport> GetSports();
    }
}
