using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using WebApp.ImageStorage.AzureBlobStorage;

namespace WebApp.Services
{
    public class ImageService
    {
        private string connectionString;
        private BlobManager manager;
        public ImageService()
        {
            connectionString = "DefaultEndpointsProtocol=https;AccountName=sportapp;AccountKey=iObMXhyrN6dbB6tIcWWqJr0Z48r" +
                "UubSPnYnxefhbF4Ek5UnKCvEgG0/12X/cNjdcZ2/iephB4jUcAch3ve3azA==;EndpointSuffix=core.windows.net";
            manager = new BlobManager(connectionString, "images");
        }
        public string UploadImage(IFormFile eventImage)
        {
            string imageName;
            using (var filestream = eventImage.OpenReadStream())
            {
                imageName = manager.UploadImage(eventImage.FileName, filestream);
            }
            return imageName;
        }
        public string GetImageURL(string imageName)
        {
            return manager.GetImageUrl(imageName);
        }
    }
}
