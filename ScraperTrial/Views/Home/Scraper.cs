using System;
using HtmlAgilityPack;
using System.Xml;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.InteropServices;
using System.Net;
using System.Text;

public class Scraper
{
    public string Scraping()
    {
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
        System.IO.File.WriteAllText(@"C:\Users\t-depra\Desktop\scraped.txt", responseText);
        return responseText;
    }
}