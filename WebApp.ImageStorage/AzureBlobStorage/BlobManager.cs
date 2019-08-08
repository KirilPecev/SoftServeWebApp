namespace WebApp.ImageStorage.AzureBlobStorage
{
    using Microsoft.Azure.Storage;
    using Microsoft.Azure.Storage.Blob;
    using System;
    using System.IO;
    public class BlobManager
    {
        private CloudStorageAccount storageAccount;
        private CloudBlobContainer blobContainer;
        public BlobManager(string connectionString, string containerName)
        {
            CloudStorageAccount account;

            if (CloudStorageAccount.TryParse(connectionString, out account))
            {
                storageAccount = account;
            }
            else
            {
                throw new ArgumentException("Connection string is wrong or missing!");
            }
            blobContainer = CreateContainer(containerName);
        }

        public string UploadImage(string fileName, Stream sourceFile)
        {
            string blobName = fileName + "_" + Guid.NewGuid().ToString() + ".jpg";
            CloudBlockBlob blockBlob = blobContainer.GetBlockBlobReference(blobName);
            blockBlob.Properties.ContentType = "image/jpg";
            blockBlob.UploadFromStream(sourceFile);
            return blobName;
        }
        public string GetImageUrl(string imageName)
        {
            var blob = blobContainer.GetBlockBlobReference(imageName);
            return blob.Uri.AbsoluteUri;
        }
        private CloudBlobContainer CreateContainer(string containerName)
        {
            CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();

            CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(containerName);

            cloudBlobContainer.CreateIfNotExists();

            BlobContainerPermissions permissions = new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            };
            cloudBlobContainer.SetPermissions(permissions);

            return cloudBlobContainer;
        }
    }
}
