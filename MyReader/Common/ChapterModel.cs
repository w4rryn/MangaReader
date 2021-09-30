using SourcePluginSDK;
using SourcePluginSDK.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Media.Imaging;

namespace MyReader.Common
{
    public class ChapterModel
    {
        public ChapterModel(Chapter chapter, Manga manga)
        {
            Chapter = chapter;
            Manga = manga;
        }

        public Chapter Chapter { get; set; }
        public Manga Manga { get; set; }
        public PluginSource ChapterSource => Manga.SourceFactory.CreateSource();
    }
}