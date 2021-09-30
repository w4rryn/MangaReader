using MyReader.Common;
using SourcePluginSDK.Domain;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyReader.Pages.ViewModels
{
    internal class SearchPageViewModel : VMBase
    {
        private readonly BackgroundWorker searchWorker;
        private ICommand searchInputTermCommand;
        private ObservableCollection<MangaModel> searchResults;
        private string searchTerm;

        public SearchPageViewModel()
        {
            searchResults = new ObservableCollection<MangaModel>();
            searchWorker = new BackgroundWorker
            {
                WorkerReportsProgress = true
            };
            searchWorker.DoWork += SearchWorkerDoWork;
            searchWorker.ProgressChanged += SearchWorkerProgressChanged;
        }

        public ICommand SearchInputTermCommand
        {
            get
            {
                if (searchInputTermCommand == null)
                {
                    searchInputTermCommand = new Command((o) => SearchInput(), () => !string.IsNullOrEmpty(SearchTerm));
                }
                return searchInputTermCommand;
            }
        }

        public ObservableCollection<MangaModel> SearchResults
        {
            get => searchResults;

            set
            {
                searchResults = value;
                OnPropertyChanged();
            }
        }

        public string SearchTerm
        {
            get { return searchTerm; }
            set
            {
                searchTerm = value;
                OnPropertyChanged();
            }
        }

        private void SearchInput()
        {
            SearchResults.Clear();
            searchWorker.RunWorkerAsync();
        }

        private void SearchWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            Task t = App.PluginManager.SearchAllSourcesAsync(SearchTerm, (m) =>
            {
                searchWorker.ReportProgress(0, m);
            });
            Task.WaitAll(t);
        }

        private void SearchWorkerProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState is Manga m)
            {
                SearchResults.Add(new MangaModel(m));
            }
        }
    }
}