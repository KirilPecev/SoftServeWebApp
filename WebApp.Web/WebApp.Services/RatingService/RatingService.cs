﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApp.Data.Repo;
using WebApp.Domain;

namespace WebApp.Services.RatingService
{
    public class RatingService : IRatingService
    {
        private IRatingRepo rating;
        public RatingService(IRatingRepo ratingRepo)
        {
            rating = ratingRepo;
        }

        public List<Rating> GetAllRatings()
        {
            return rating.GetAllRatings().ToList();
        }
    }
}