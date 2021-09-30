using HtmlAgilityPack;
using MangakakalotPlugin.Domain;
using SourcePluginSDK.Domain;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace MangakakalotPlugin.Actions
{
    internal class MangaSearch
    {
        private const string searchQueryBase = "https://mangakakalot.com/search/story/";
        private const string searchResultXPathSelector = "/html/body/div[1]/div[2]/div[1]/div[2]/div/div[*]";

        private string GenerateSearchQuery(string search)
        {
            return searchQueryBase + search.Replace(' ', '_');
        }

        public void SearchManga(string rawString, Action<Manga> searchCallback)
        {
            HtmlNodeCollection nodes = GetSearchResultCollection(rawString);
            foreach (HtmlNode storyItem in nodes)
            {
                HtmlNode itemRight = storyItem.ChildNodes.Single(x => x.Name == "div");
                HtmlNode a = storyItem.ChildNodes.Single(x => x.Name == "a");
                Bitmap thumb = GetThumbnailFromNode(a);
                var title = GetMangaTitleFromNode(itemRight);
                var url = a.GetAttributeValue("href", "");
                var mg = new MgkkManga(title, url, thumb);
                searchCallback(mg);
            }
        }

        private static Bitmap GetThumbnailFromNode(HtmlNode a)
        {
            HtmlNode img = a.ChildNodes.Single(x => x.Name == "img");
            var thumbURL = img.GetAttributeValue("src", "");
            Bitmap thumb = Helper.GetBitmapFromUrl(thumbURL);
            return thumb;
        }

        private static string GetMangaTitleFromNode(HtmlNode itemRight)
        {
            return itemRight.Descendants().Where(x => x.Name == "a").Single(x => x.ParentNode.Name == "h3").InnerText;
        }

        private HtmlNodeCollection GetSearchResultCollection(string rawString)
        {
            var query = GenerateSearchQuery(rawString);
            HtmlDocument doc = Helper.GetHtmlDocFromUrl(query);
            return doc.DocumentNode.SelectNodes(searchResultXPathSelector);
        }
    }
}