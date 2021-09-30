using SourcePluginSDK;
using System;

namespace MangakakalotPlugin
{
    [Serializable]
    public class MangakakalotFactory : ISourceFactory
    {
        public PluginSource CreateSource()
        {
            return new Main();
        }

        public override string ToString()
        {
            return "Mangakakalot";
        }
    }
}