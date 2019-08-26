namespace WebApp.Data.Repo.ScoreRepo
{
    using Domain;
    using System;
    using System.Collections.Generic;

    public interface IScoreRepo
    {
        IEnumerable<Rating> GetAllData();

        IEnumerable<int> GetScores(List<int> scores);

        IEnumerable<string> GetDates(List<DateTime> dates);
    }
}
