using System;
using System.Collections.Generic;
using System.Text;
using WebApp.Domain;

namespace WebApp.Services.ScoreService
{
    public interface IScoreService
    {
        List<Rating> GetAllData();
    }
}
