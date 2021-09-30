using HtmlAgilityPack;
using System;
using System.Drawing;
using System.IO;
using System.Net;

namespace MangakakalotPlugin
{
    [Serializable]
    internal static class Helper
    {
        internal static Bitmap GetBitmapFromUrl(string panelUrl, string refererHeader = "")
        {
            var request = (HttpWebRequest)WebRequest.Create(panelUrl);
            request.Referer = refererHeader;
            using WebResponse response = request.GetResponse();
            using Stream responseStream = response.GetResponseStream();
            return new Bitmap(responseStream);
        }

        internal static HtmlDocument GetHtmlDocFromUrl(string url)
        {
            using var client = new WebClient();
            client.UseDefaultCredentials = true;
            var data = client.DownloadString(url);
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(data);
            return htmlDoc;
        }
    }
}