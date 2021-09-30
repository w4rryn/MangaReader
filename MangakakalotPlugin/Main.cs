using MangakakalotPlugin.Actions;
using MangakakalotPlugin.Domain;
using SourcePluginSDK;
using SourcePluginSDK.Domain;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace MangakakalotPlugin
{
    [Serializable]
    public class Main : PluginSource
    {
        private static readonly PluginMetadata metadata = new PluginMetadata("w4rryn", "v.1.0.0", "Default source plugin", "");
        private readonly MangaSearch searchAction;
        private readonly ChapterLister chapterLister;
        private readonly Reader mangaReader;

        public Main() : base(metadata)
        {
            searchAction = new MangaSearch();
            chapterLister = new ChapterLister();
            mangaReader = new Reader();
        }

        public override IEnumerable<Chapter> GetChapters(Manga manga)
        {
            return chapterLister.GetChapters(manga);
        }

        public override void GetPanels(Manga manga, Chapter chapter, Action<Panel> panelLoadedCallback)
        {
            mangaReader.GetPanels((MgkkChapter)chapter, panelLoadedCallback);
        }

        public override ISourceFactory GetSourceFactory()
        {
            return new MangakakalotFactory();
        }

        public override void SearchManga(string rawString, Action<Manga> searchCallback)
        {
            searchAction.SearchManga(rawString, searchCallback);
        }
    }
}