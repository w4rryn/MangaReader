using HtmlAgilityPack;
using MangakakalotPlugin.Domain;
using SourcePluginSDK.Domain;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace MangakakalotPlugin.Actions
{
    internal class Reader
    {
        public Reader()
        {
        }

        private const string refererHeader = "https://mangakakalot.com/";
        private const string chapterPanelXPathSelector = "/html/body/div[1]/div[3]/img";

        internal void GetPanels(MgkkChapter chapter, Action<Panel> panelLoadedCallback)
        {
            var index = 0;
            HtmlDocument htmlDoc = Helper.GetHtmlDocFromUrl(chapter.Url);
            HtmlNodeCollection images = htmlDoc.DocumentNode.SelectNodes(chapterPanelXPathSelector);
            foreach (HtmlNode img in images)
            {
                var source = img.GetAttributeValue("src", "");
                Bitmap map = Helper.GetBitmapFromUrl(source, refererHeader);
                panelLoadedCallback(new Panel(index, map));
                index++;
            }
        }
    }
}