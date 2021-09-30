using MyReader.Common;
using MyReader.Pages.ViewModels;
using SourcePluginSDK;
using SourcePluginSDK.Domain;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;

namespace MyReader.Pages
{
    /// <summary>
    /// Interaction logic for SearchPage.xaml
    /// </summary>
    public partial class SearchPage : Page
    {
        public SearchPage()
        {
            InitializeComponent();
        }

        private void LbSearchResultsMouseDoubleClick_OpenChapterView(object sender, MouseButtonEventArgs e)
        {
            var selected = lbSearchResults.SelectedItem as MangaModel;
            PluginSource source = selected.GetSourceInstance();
            IEnumerable<Chapter> chapters = source.GetChapters(selected.Manga);
            var page = new MangaPage
            {
                DataContext = new MangaPageViewModel(selected, chapters)
            };
            App.SwitchContent(page);
        }
    }
}