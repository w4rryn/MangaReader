using System;
using System.Drawing;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace MyReader.Common
{
    internal static class Helper
    {
        public static BitmapSource ConvertBitmapToImageSource(Bitmap panel)
        {
            IntPtr handle = panel.GetHbitmap();
            BitmapSource source = Imaging.CreateBitmapSourceFromHBitmap(handle, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            return source;
        }
    }
}