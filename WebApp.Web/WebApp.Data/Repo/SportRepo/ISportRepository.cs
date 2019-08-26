namespace WebApp.Data.Repo.SportRepo
{
    using Domain;
    using System.Collections.Generic;

    public interface ISportRepository
    {
        IEnumerable<Sport> GetSports();
    }
}
