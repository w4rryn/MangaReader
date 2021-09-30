using SourcePluginSDK.Domain;
using System.Windows.Media.Imaging;

namespace MyReader.Common
{
    internal class PanelModel : VMBase
    {
        public Panel Panel { get; set; }
        public BitmapSource Source { get; set; }

        public PanelModel(Panel p)
        {
            Panel = p;
            Source = Helper.ConvertBitmapToImageSource(p.PanelImage);
        }
    }
}