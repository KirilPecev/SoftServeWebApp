namespace WebApp.Data.Repo.ScoreRepo
{
    using Domain;
    using System;
    using System.Collections.Generic;

    public interface IScoreRepository
    {
        IEnumerable<Rating> GetAllData();

        IEnumerable<string> GetDates(List<DateTime> dates);
    }
}
