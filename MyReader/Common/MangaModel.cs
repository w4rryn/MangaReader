using SourcePluginSDK;
using SourcePluginSDK.Domain;
using System.Windows.Media.Imaging;

namespace MyReader.Common
{
    public class MangaModel
    {
        public MangaModel(Manga manga)
        {
            Manga = manga;
            Thumbnail = Helper.ConvertBitmapToImageSource(manga.Thumbnail);
        }

        public Manga Manga { get; }
        public BitmapSource Thumbnail { get; }
        public string Title { get => Manga.Title; }
        public string Author { get => Manga.Author; }
        public string Source { get => Manga.SourceFactory.ToString(); }

        public PluginSource GetSourceInstance()
        {
            return Manga.SourceFactory.CreateSource();
        }
    }
}