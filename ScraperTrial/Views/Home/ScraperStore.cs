using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.IO;
using System.Threading.Tasks;

public class ScraperFileStore
{
    public void SaveScrapedFile()
    {
        string connectionString = "DefaultEndpointsProtocol=https;AccountName=scrapertrialstorage;AccountKey=lrP3oq1JRMU7sYsCDs4CHzL83VqPExKL4YW46aVcQzByG/2z2eNKFLFa2Eocge67/CO/zEklNKXnKKoWU/gQPw==;EndpointSuffix=core.windows.net";
        CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);
        CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
        CloudBlobContainer container = blobClient.GetContainerReference("scrapedfiles");
        CloudBlockBlob blockBlob = container.GetBlockBlobReference("scrapertrialstorage");
        using (var fileStream = System.IO.File.OpenRead(@"C:\Users\t-depra\Desktop\scraped.txt"))
        {
            blockBlob.UploadFromStreamAsync(fileStream);
        }
    }
}


// tried the approach below but it's incomplete/did not work 
/*
namespace ScraperTrial.Services
{
    public class ScraperFileStore
    {
        CloudBlobClient blobClient;
        string baseUri = "https://scrapertrialstorage.blob.core.windows.net/";
        public ScraperFileStore()
        {
            var credentials = new StorageCredentials("scrapertrialstorage", "V8lAf4DWsjSt2qJ4JwfyCaw7TKkTM2uKrtQ21B5y8Q7VqLxKUpGe8/9VpnqNFgiLndz1fa5DLsgd/ODfEg0ZHg==");
            blobClient = new CloudBlobClient(new Uri(baseUri), credentials);
        }

        public Task<string> SaveScrapedFile(Stream fileStream)
        {
            var container = blobClient.GetContainerReference("scrapedfiles");
            var blob = container.GetBlockBlobReference()
        }
    }
}

*/


