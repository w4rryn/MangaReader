using MyReader.Common;
using SourcePluginSDK.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace MyReader.Pages.ViewModels
{
    internal class MangaPageViewModel : VMBase
    {
        private ObservableCollection<Chapter> mangaChapters;
        private Chapter selectedChapter;
        private MangaModel selectedManga;
        private ICommand switchToChapterReader;

        public MangaPageViewModel()
        {
        }

        public MangaPageViewModel(MangaModel manga, IEnumerable<Chapter> chapters) : this()
        {
            SelectedManga = manga;
            MangaChapters = new ObservableCollection<Chapter>(chapters);
        }

        public ObservableCollection<Chapter> MangaChapters
        {
            get => mangaChapters;

            set
            {
                mangaChapters = value;
                OnPropertyChanged();
            }
        }

        public Chapter SelectedChapter
        {
            get { return selectedChapter; }
            set
            {
                selectedChapter = value;
                OnPropertyChanged();
            }
        }

        public MangaModel SelectedManga
        {
            get => selectedManga;
            set
            {
                selectedManga = value;
                OnPropertyChanged();
            }
        }

        public ICommand SwitchToChapterReaderCommand
        {
            get
            {
                if (switchToChapterReader == null)
                {
                    switchToChapterReader = new Command((o) =>
                    {
                        var p = new MangaReaderPage
                        {
                            DataContext = new MangaReaderPageViewModel(new ChapterModel(SelectedChapter, SelectedManga.Manga))
                        };
                        App.SwitchContent(p);
                    }, () => SelectedChapter != null);
                }
                return switchToChapterReader;
            }
        }
    }
}