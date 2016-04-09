using System;
using GalaSoft.MvvmLight.Command;
using MangaLight.ViewModel.CollectionViewModel;
using MangaLight.ViewModel.ControlViewModel;
using System.Diagnostics;
using Windows.UI.Popups;
using Windows.Storage;
using Windows.Storage.Pickers;
using System.Linq;
using MangaLight.Model;

namespace MangaLight.ViewModel
{
    public class MangaDetailViewModel : MangaViewModel
    {
        #region Fields
        private ChapterCollectionViewModel _chapterCollection;

        private bool _isDownloadSeriesCompleted = false;

        enum DownLoadType
        {
            DownloadSeriesWithLocalFolderCommandId,
            DownloadSelectedChaptersWithLocalFolderCommandId,
            DownloadSeriesWithSpecifyFolderCommandId,
            DownloadSelectedChapterWithSpecifyFolderCommandId,
        }
        #endregion

        #region Properties

        public ChapterCollectionViewModel ChapterCollection
        {
            get { return _chapterCollection; }
            set { _chapterCollection = value; }
        }

        public bool IsDownloadSeriesCompleted
        {
            get { return _isDownloadSeriesCompleted; }
            set { _isDownloadSeriesCompleted = value; }
        }

        public RelayCommand DownloadSelectedChaptersCommand { get; set; }

        public RelayCommand DownloadSeriesCommand { get; set; }

        public RelayCommand DownloadChapterCommand { get; set; }

        #endregion

        public MangaDetailViewModel(Manga manga, MangaSource mangaSource)
        {
            _manga = manga;
            _mangaSource = mangaSource;
            LoadingInitializationData();
        }

        public MangaDetailViewModel(MangaViewModel mangaViewModel)
        {
            _manga = mangaViewModel.Manga;
            _mangaSource = mangaViewModel.MangaSource;



        }

        private void LoadingInitializationData()
        {
            LoadImage();
            ChapterCollection = new ChapterCollectionViewModel(_mangaSource, _manga, 10);
            CreateCommand();
        }

        private void CreateCommand()
        {
            DownloadChapterCommand = new RelayCommand(() => DownloadChapter(), () => CanDownloadChapter());
            DownloadSelectedChaptersCommand = new RelayCommand(() => DownloadSelectedChapters(), () => CanDownloadSelectedChapters());
            DownloadSeriesCommand = new RelayCommand(() => DownloadSeries(), () => CanDownloadSeries());
        }

        #region Download Series

        private bool CanDownloadSeries()
        {            
            return true;
        }

        private async void DownloadSeries()
        {
            MessageDialog dialog = new MessageDialog("You are downloading FULL MANGA, select folder to download..." +
                    "\nNote: Download with local folder will have many benefit for you." +
                    "\n\t1/Auto update manga." +
                    "\n\t2/Manga will in your collection.");
            dialog.Commands.Add(new UICommand("Local folder", Download, DownLoadType.DownloadSeriesWithLocalFolderCommandId));
            dialog.Commands.Add(new UICommand("Specify folder", Download, DownLoadType.DownloadSeriesWithSpecifyFolderCommandId));
            dialog.Commands.Add(new UICommand("Cancel"));
            dialog.CancelCommandIndex = 2;
            dialog.DefaultCommandIndex = 0;

            await dialog.ShowAsync();
        }

        private async void Download(IUICommand command)
        {
            var commandId = (DownLoadType)command.Id;
            try
            {
                if (commandId == DownLoadType.DownloadSeriesWithLocalFolderCommandId)
                {
                    IsDownloadSeriesCompleted = await _mangaSource.DownloadMangaWithLocalFolderAsync(_manga);
                }
                else if (commandId == DownLoadType.DownloadSeriesWithSpecifyFolderCommandId)
                {
                    IsDownloadSeriesCompleted = await _mangaSource.DownloadMangaWithSpecifyFolderAsync(_manga);
                }
                else if (commandId == DownLoadType.DownloadSelectedChaptersWithLocalFolderCommandId)
                {
                    var selectedChaptersViewModel = ChapterCollection.Where(p => p.IsSelected == true);
                    var chapters = selectedChaptersViewModel.Select(p => p.GetChapter()).ToList();
                    IsDownloadSeriesCompleted = await _mangaSource.DownloadMangaWithLocalFolderAsync(_manga, chapters);
                }
                else if (commandId == DownLoadType.DownloadSelectedChapterWithSpecifyFolderCommandId)
                {
                    var selectedChaptersViewModel = ChapterCollection.Where(p => p.IsSelected == true);
                    var chapters = selectedChaptersViewModel.Select(p => p.GetChapter()).ToList();
                    IsDownloadSeriesCompleted = await _mangaSource.DownloadMangaWithSpecifyFolderAsync(_manga, chapters);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("MangaDetailViewModel - Download Series With Local Folder - Exception: " + ex.Message);
            }
            OnPropertyChanged("IsDownloadSeriesCompleted");
        }
        #endregion

        #region Download Chapters

        private bool CanDownloadSelectedChapters()
        {
            return true;
        }

        private async void DownloadSelectedChapters()
        {
            //var selectedChapters = ChapterCollection.Where(p => p.IsSelected == true);
            MessageDialog dialog = new MessageDialog("You are downloading SELECTED CHAPTER, select folder to download..." +
                "\nNote: Download with local folder will have many benefit for you." +
                "\n\t1/Auto update manga." +
                "\n\t2/Manga will in your collection.");
            dialog.Commands.Add(new UICommand("Local folder", Download, DownLoadType.DownloadSelectedChaptersWithLocalFolderCommandId));
            dialog.Commands.Add(new UICommand("Specify folder", Download, DownLoadType.DownloadSelectedChapterWithSpecifyFolderCommandId));
            dialog.Commands.Add(new UICommand("Cancel"));
            dialog.CancelCommandIndex = 2;
            dialog.DefaultCommandIndex = 0;
            await dialog.ShowAsync();
        }

        #endregion

        #region Download Chapter

        private bool CanDownloadChapter()
        {
            return true;
        }

        private void DownloadChapter()
        {
            throw new NotImplementedException();
        }

        public bool isTheSameManga(MangaDetailViewModel obj)
        {
            if(obj != null)
            {
                if (obj.Manga == this.Manga && obj.MangaSource == this.MangaSource)
                    return true;
            }
            return false;
        }

        #endregion

    }

}
