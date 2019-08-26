namespace WebApp.Services.SportService
{
    using Domain;
    using System.Collections.Generic;

    public interface ISportService
    {
        IEnumerable<Sport> GetSports();
    }
}
