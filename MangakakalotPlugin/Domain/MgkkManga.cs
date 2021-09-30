﻿using SourcePluginSDK;
using SourcePluginSDK.Domain;
using System.Drawing;

namespace MangakakalotPlugin.Domain
{
    public class MgkkManga : Manga
    {
        private static readonly ISourceFactory source = new MangakakalotFactory();

        public MgkkManga(string title, string url, Bitmap thumbnail) : base(source)
        {
            Title = title;
            Url = url;
            Thumbnail = thumbnail;
        }

        public string Url { get; }
    }
}