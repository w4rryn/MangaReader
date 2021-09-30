using MyReader.Common;
using MyReader.Core;
using MyReader.Pages;
using System.Windows;
using System.Windows.Controls;

namespace MyReader
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static IContentSwitcher switcher;
        public const string PLUGIN_DIR = "plugins";
        public static PluginLoader PluginManager { get; private set; }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            PluginManager = InitPlugins();
            var win = new MainWindow();
            switcher = win;
            win.Show();
            switcher.SwitchContent(new SearchPage());
        }

        private PluginLoader InitPlugins()
        {
            var pluginManager = new PluginLoader();
            pluginManager.LoadPlugins();
            return pluginManager;
        }

        public static void SwitchContent(Page p)
        {
            switcher.SwitchContent(p);
        }
    }
}