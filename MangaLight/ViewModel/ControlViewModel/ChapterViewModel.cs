using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MangaLight.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaLight.ViewModel.ControlViewModel
{
    public class ChapterViewModel : ViewModelBase
    {

        #region Fields

        private Manga _manga;

        private Chapter _chapter;

        private MangaSource _mangaSource;

        private bool _isDownloadCompleted;

        #endregion

        #region Properties

        public MangaSource MangaSource
        {
            get { return _mangaSource; }
            set { _mangaSource = MangaSource; }
        }

        public string ChapterName
        {
            get
            {
                return _chapter.ChapterName;
            }
        }

        public RelayCommand DownloadCommand { get; set; }
        
        public bool IsDownloadCompleted
        {
            get { return _isDownloadCompleted; }
            set { Set<bool>(() => IsDownloadCompleted, ref _isDownloadCompleted, value); }
        }

        public Uri ChapterUri
        {
            get
            {
                return _chapter.ChapterUri;
            }
        }

        private bool _isSelected;

        public bool IsSelected
        {
            get { return _isSelected; }
            set { Set<bool>(() => IsSelected, ref _isSelected, value); }
        }

        #endregion

        public ChapterViewModel(MangaSource mangaSource, Manga manga, Chapter chapter)
        {
            _chapter = chapter;
            _manga = manga;
            _mangaSource = mangaSource;
            DownloadCommand = new RelayCommand(()=>DownloadChapter());
        }

        async void DownloadChapter()
        {
            try
            {
                bool isDownloadCompleted = await _mangaSource.DownloadChapterAsync(_chapter);
            }
            catch (Exception)
            {
                Debug.WriteLine("Download chapter " + _chapter.ChapterName + " Error.");
            }
        }

        public Chapter GetChapter()
        {
            return _chapter;
        }
    }
}
