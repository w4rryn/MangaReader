using SourcePluginSDK.Domain;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace SourcePluginSDK
{
    [Serializable]
    public abstract class PluginSource
    {
        public PluginSource(PluginMetadata meta)
        {
            Metadata = meta;
        }

        public PluginMetadata Metadata { get; }

        public abstract ISourceFactory GetSourceFactory();

        public abstract void SearchManga(string rawString, Action<Manga> searchCallback);

        public abstract IEnumerable<Chapter> GetChapters(Manga manga);

        public abstract void GetPanels(Manga manga, Chapter chapter, Action<Panel> panelLoadedCallback);
    }
}