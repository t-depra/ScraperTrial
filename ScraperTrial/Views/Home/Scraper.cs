using System;
using HtmlAgilityPack;
using System.Xml;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.InteropServices;
using System.Net;
using System.Text;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

public class Scraper
{
    IConfiguration _configuration;
    public Scraper(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string Scraping()
    {
        // Scraping webpage 
        string url = _configuration["url"];
        HttpWebRequest HttpWReq = (HttpWebRequest)WebRequest.Create(url);
        HttpWebResponse HttpWResp = (HttpWebResponse)HttpWReq.GetResponse();

        var encoding = ASCIIEncoding.ASCII;
        string responseText;
        using (var reader = new System.IO.StreamReader(HttpWResp.GetResponseStream(), encoding))
        {
            responseText = reader.ReadToEnd();
        }
        HttpWResp.Close();

        // Storing to Blob Storage 
        string connectionString = _configuration.GetSection("Data").GetSection("ConnectionString").Value;
        CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);
        CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
        CloudBlobContainer container = blobClient.GetContainerReference("scrapedfiles");
        CloudBlockBlob blockBlob = container.GetBlockBlobReference("scrapertrialstorage");
        blockBlob.UploadTextAsync(responseText);

        return responseText;
    }
}