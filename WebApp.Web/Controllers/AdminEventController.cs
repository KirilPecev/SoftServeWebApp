﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using WebApp.ImageStorage.AzureBlobStorage;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;

namespace WebApp.Web.Controllers
{
    public class AdminEventController : Controller
    {
        [HttpGet]
        public IActionResult AdminViewEvent()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(string eventSport, string title, string description,DateTime time, IFormFile eventImage)
        {
            return View();
        }
        private static string UploadImage(IFormFile eventImage)
        {
            string imageName;
            string connectionString = "DefaultEndpointsProtocol=https;AccountName=sportapp;AccountKey=iObMXhyrN6dbB6tIcWWqJr0Z48rUubSPnYnxefhbF4Ek5UnKCvEgG0/12X/cNjdcZ2/iephB4jUcAch3ve3azA==;EndpointSuffix=core.windows.net";
            BlobManager manager = new BlobManager(connectionString, "images");
            using (var filestream = eventImage.OpenReadStream())
            {
                imageName = manager.UploadImage(eventImage.FileName, filestream);
            }
            return imageName;
        }
    }
}