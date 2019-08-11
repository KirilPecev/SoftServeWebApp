using System;
using System.Collections.Generic;
using System.Text;
using WebApp.Domain;

namespace WebApp.Services.RatingService
{
    public interface IRatingService
    {
        List<Rating> GetAllRatings();
    }
}
