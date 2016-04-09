using GalaSoft.MvvmLight;
using MangaLight.Model;
using MangaLight.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using Windows.Web.Http;
using Windows.ApplicationModel;

namespace MangaLight.ViewModel.ControlViewModel
{
    public class MangaViewModel : INotifyPropertyChanged
    {
        #region Fields
        protected Manga _manga;

        protected MangaSource _mangaSource;

        private bool _isLoadingCover;

        private bool _isHaveGenreInfo = false;

        private bool _isHaveAuthorInfo = false;

        private bool _isHaveStatusInfo = false;

        private string _mangaName;


        #endregion

        #region Propeties
        public Manga Manga
        {
            get { return _manga; }
            set { _manga = value; }
        }

        public MangaSource MangaSource
        {
            get { return _mangaSource; }
            set { _mangaSource = value; }
        }

        public Uri CoverUri
        {
            get;set;
        }       

        public bool IsLoadingCover
        {
            get { return _isLoadingCover; }
            set { _isLoadingCover = value; }
        }

        #region Binding Data

        public string MangaName
        {
            get
            {
                return _mangaName;
            }
            set
            {
                _mangaName = value;
                OnPropertyChanged("MangaName");
            }
        }

        public string Author
        {
            get
            {
                var author = _manga.MangaInfo.Author;
                if(author != null)
                {
                    IsHaveAuthorInfo = true;
                }
                return author;
            }
        }

        public string Genre
        {
            get
            {
                var GenreListName = _manga.MangaInfo.GenreListName;
                string name = String.Empty;
                if (GenreListName != null)
                {
                    IsHaveGenreInfo = true;
                    for (int i = 0; i < GenreListName.Count; i++)
                    {
                        if (i != GenreListName.Count - 1)
                        {
                            name += GenreListName[i] + ", ";
                        }
                        else
                        {
                            name += GenreListName[i];
                        }
                    }
                }
                return name;
            }

        }

        public string LatestChapter
        {
            get
            {
                return _manga.MangaInfo.LatestChapter;
            }
        }

        public string Status
        {
            get
            {
                var status = _manga.MangaInfo.Status;
                if (status == null)
                    IsHaveStatusInfo = false;
                return status;
            }
        }

        public string Summary
        {
            get
            {
                return _manga.MangaInfo.Summary;
            }
        }

        #endregion

        #region Converter

        public bool IsHaveGenreInfo
        {
            get { return _isHaveGenreInfo; }
            set
            {
                _isHaveGenreInfo = value;
                OnPropertyChanged("IsHaveGenreInfo");
            }
        }

        public bool IsHaveAuthorInfo
        {
            get { return _isHaveAuthorInfo; }
            set
            {
                _isHaveAuthorInfo = value;
                OnPropertyChanged("IsHaveAuthorInfo");
            }
        }


        public bool IsHaveStatusInfo
        {
            get { return _isHaveStatusInfo; }
            set
            {
                _isHaveStatusInfo = value;
                OnPropertyChanged("IsHaveStatusInfo");
            }
        }
        #endregion

        #endregion

        #region Constructor

        public MangaViewModel()
        {
            if (DesignMode.DesignModeEnabled)
            {
                _manga = new Manga
                {
                    MangaInfo = new MangaInfo
                    {
                        MangaName = "009 Re:Cyborg",
                        Author = "Ishinomori Shotaro ; Kamiyama Kenji,Asou Gatou",
                        GenreListName = new List<string> { "Action", "Comedy", "Shoujo", },
                        Status = "On Going",
                        CoverImage = new Uri("http://truyentranhtuan.com/wp-content/uploads/2015/08/bt3603-009-re-cyborg2-200x304.jpg"),
                    },
                };
            }
        }

        public MangaViewModel(Manga manga)
        {
            _manga = manga;
            LoadImage();
        }

        public MangaViewModel(MangaSource mangaSource, Manga manga)
        {
            _mangaSource = mangaSource;
            _manga = manga;
            MangaName = _manga.MangaInfo.MangaName;
            LoadImage();
        }
        #endregion

        #region Methods

        public async void LoadImage()
        {
            _isLoadingCover = true;
            OnPropertyChanged("IsLoadingCover");
            var uri = _manga.MangaInfo.CoverImage;
            //var uri = await _mangaSource.GetMangaImageUri(_manga);
            var notFoundUri = new Uri(@"ms-appx:/Assets/ImageNotFound.jpg");
            CoverUri = uri;
            #region Reserve
            if (uri != null)
            {
                try
                {
                    var response = await IDataService.GetResponse(uri);
                    if ((response as HttpResponseMessage).IsSuccessStatusCode)
                    {
                        CoverUri = uri;
                    }
                    else
                    {
                        CoverUri = notFoundUri;
                    }
                }
                catch (Exception)
                {
                    CoverUri = notFoundUri;
                    _isLoadingCover = false;
                    OnPropertyChanged("CoverUri");
                    OnPropertyChanged("IsLoadingCover");
                }
            }
            else
                CoverUri = notFoundUri;
            #endregion
            _isLoadingCover = false;
            OnPropertyChanged("CoverUri");
            OnPropertyChanged("IsLoadingCover");
        }
        #endregion

        #region Event
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}
