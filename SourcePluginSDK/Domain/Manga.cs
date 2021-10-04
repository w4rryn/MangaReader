using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SourcePluginSDK.Domain
{
    [Serializable]
    public abstract class Manga
    {
        public Manga(ISourceFactory source)
        {
            SourceFactory = source;
        }

        public string Author { get; set; }
        public string Title { get; set; }
        public Bitmap Thumbnail { get; set; }
        public ISourceFactory SourceFactory { get; protected set; }
        public PluginSource Source { get => SourceFactory.CreateSource(); }
    }
}