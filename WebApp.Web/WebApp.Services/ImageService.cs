namespace WebApp.Services
{
    using ImageStorage.AzureBlobStorage;
    using Microsoft.AspNetCore.Http;

    public class ImageService
    {
        private readonly BlobManager manager;

        public ImageService()
        {
            var connectionString = "DefaultEndpointsProtocol=https;AccountName=sportapp;AccountKey=iObMXhyrN6dbB6tIcWWqJr0Z48r" +
                                      "UubSPnYnxefhbF4Ek5UnKCvEgG0/12X/cNjdcZ2/iephB4jUcAch3ve3azA==;EndpointSuffix=core.windows.net";
            manager = new BlobManager(connectionString, "images");
        }

        public string UploadImage(IFormFile eventImage)
        {
            string imageName;
            using (var fileStream = eventImage.OpenReadStream())
            {
                imageName = manager.UploadImage(eventImage.FileName, fileStream);
            }
            return imageName;
        }

        public string GetImageUrl(string imageName)
        {
            return manager.GetImageUrl(imageName);
        }
    }
}
