using SourcePluginSDK.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MangakakalotPlugin.Domain
{
    public class MgkkChapter : Chapter
    {
        public MgkkChapter(string name, string url) : base(name)
        {
            Url = url;
        }

        public string Url { get; }
    }
}