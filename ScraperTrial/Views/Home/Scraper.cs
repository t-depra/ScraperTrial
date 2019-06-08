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

public class Scraper
{
    public string Scraping()
    {
        // Scraping webpage 
        string BingMapsKey = "AvCBj_jgkzN6BB4bx-dGT-vQkOoLtvPrApuuZzIem1pGoAyfbbwgL_JPGZsNNY28";
        string url = "http://dev.virtualearth.net/REST/V1/Routes/Driving?o=xml&wp.0=london&wp.1=leeds&avoid=minimizeTolls&key=" + BingMapsKey;
        HttpWebRequest HttpWReq = (HttpWebRequest)WebRequest.Create(url);
        HttpWebResponse HttpWResp = (HttpWebResponse)HttpWReq.GetResponse();

        var encoding = ASCIIEncoding.ASCII;
        string responseText;
        using (var reader = new System.IO.StreamReader(HttpWResp.GetResponseStream(), encoding))
        {
            responseText = reader.ReadToEnd();
        }
        HttpWResp.Close();
        // System.IO.File.WriteAllText(@"C:\Users\t-depra\Desktop\scraped.txt", responseText);

        // Storing to Blob Storage 
        string connectionString = "DefaultEndpointsProtocol=https;AccountName=scrapertrialstorage;AccountKey=lrP3oq1JRMU7sYsCDs4CHzL83VqPExKL4YW46aVcQzByG/2z2eNKFLFa2Eocge67/CO/zEklNKXnKKoWU/gQPw==;EndpointSuffix=core.windows.net";
        CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);
        CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
        CloudBlobContainer container = blobClient.GetContainerReference("scrapedfiles");
        CloudBlockBlob blockBlob = container.GetBlockBlobReference("scrapertrialstorage");
        blockBlob.UploadTextAsync(responseText);

        return responseText;
    }
}