using System.Drawing;

namespace SourcePluginSDK.Domain
{
    public class Panel
    {
        public Panel(int panelNumber, Bitmap data)
        {
            PanelNumber = panelNumber;
            PanelImage = data;
        }

        public int PanelNumber { get; }
        public Bitmap PanelImage { get; }
    }
}