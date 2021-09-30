using HtmlAgilityPack;
using MangakakalotPlugin.Domain;
using SourcePluginSDK.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MangakakalotPlugin.Actions
{
    internal class ChapterLister
    {
        private const string availableChaptersXPathSelector = "/html/body/div[1]/div[3]/div[1]/div[3]/ul/li[*]";

        public IEnumerable<Chapter> GetChapters(Manga manga)
        {
            var results = new List<Chapter>();
            if (manga is MgkkManga mg)
            {
                HtmlNodeCollection chapters = GetChapterCollectionFromUrl(mg.Url);
                foreach (HtmlNode chap in chapters)
                {
                    results.Add(CreateChapterFromHtmlNode(chap));
                }
            }
            else
            {
                throw new ArgumentException("Invalid manga object");
            }
            return results;
        }

        private HtmlNodeCollection GetChapterCollectionFromUrl(string url)
        {
            HtmlDocument htmlDoc = Helper.GetHtmlDocFromUrl(url);

            return htmlDoc.DocumentNode.SelectNodes(availableChaptersXPathSelector);
        }

        private Chapter CreateChapterFromHtmlNode(HtmlNode chap)
        {
            HtmlNode a = chap.Descendants().First(x => x.Name == "a");
            var url = a.GetAttributeValue("href", "");
            var name = a.InnerText;
            return new MgkkChapter(name, url);
        }
    }
}