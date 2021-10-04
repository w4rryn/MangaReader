using MyReader.Common;
using MyReader.Core;
using SourcePluginSDK.Domain;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyReader.Pages.ViewModels
{
    internal class MangaReaderPageViewModel : VMBase
    {
        private readonly BackgroundWorker panelLoaderWorker;
        private ObservableCollection<PanelModel> chapterPanels;
        private ChapterModel currentChapter;
        private readonly Bookmarker bookmarker;

        public MangaReaderPageViewModel()
        {
            ChapterPanels = new ObservableCollection<PanelModel>();
            panelLoaderWorker = new BackgroundWorker();
            panelLoaderWorker.DoWork += PanelLoaderWorker_DoWork;
            panelLoaderWorker.ProgressChanged += PanelLoaderWorker_ProgressChanged;
            panelLoaderWorker.WorkerReportsProgress = true;
        }

        public MangaReaderPageViewModel(ChapterModel chapter) : this()
        {
            CurrentChapter = chapter;
            bookmarker = new Bookmarker();
        }

        public ObservableCollection<PanelModel> ChapterPanels
        {
            get { return chapterPanels; }
            set
            {
                chapterPanels = value;
                OnPropertyChanged();
            }
        }

        private ICommand setBookmarkCommand;

        public ICommand SetBookmarkCommand
        {
            get
            {
                if (setBookmarkCommand == null)
                {
                    setBookmarkCommand = new Command((o) => CreateBookmarkForCurrentChapter(), () => true);
                }
                return setBookmarkCommand;
            }
        }

        private void CreateBookmarkForCurrentChapter()
        {
            bookmarker.CreateBookmark(currentChapter.Manga, currentChapter.Chapter);
        }

        public ChapterModel CurrentChapter
        {
            get => currentChapter; set
            {
                currentChapter = value;
                ChapterPanels.Clear();
                panelLoaderWorker.RunWorkerAsync();
                OnPropertyChanged();
            }
        }

        private void PanelLoaderWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Task t = App.PluginManager.LoadPanelsFromChapter(currentChapter.Manga, currentChapter.Chapter, (o) =>
            {
                panelLoaderWorker.ReportProgress(0, o);
            });
            Task.WaitAll(t);
        }

        private void PanelLoaderWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState is Panel p)
            {
                ChapterPanels.Add(new PanelModel(p));
            }
        }
    }
}