﻿namespace WebApp.Web.Models.Event
{
    using System;
    using System.Collections.Generic;

    public class EventBindingModel
    {
        public int Id { get; set; }
        public EventBindingModel()
        {
            Positions = new List<PositionModel>();
        }

        public string Title { get; set; }

        public string ImageName { get; set; }

        public string ImageURL { get; set; }

        public string AdminId { get; set; }

        public string AdminName { get; set; }

        public string Location { get; set; }

        public string Description { get; set; }

        public DateTime Time { get; set; }

        public int SportId { get; set; }

        public List<PositionModel> Positions { get; set; }
    }
}
