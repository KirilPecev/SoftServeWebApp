using System;
using System.Collections.Generic;
using WebApp.Domain;

namespace WebApp.Data.Repo
{
    public interface IScoreRepo
    {
        List<Rating> GetAllData();
        List<int> GetScores(List<int> scores);
        List<string> GetDates(List<DateTime> dates);
    }
}
