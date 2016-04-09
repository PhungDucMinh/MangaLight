using MangaLight.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Data;
using System;
using Windows.Foundation;
using System.Threading.Tasks;
using MangaLight.Service;
using System.Diagnostics;
using System.Linq;
using GalaSoft.MvvmLight;
using System.ComponentModel;
using MangaLight.ViewModel.ControlViewModel;

namespace MangaLight.ViewModel.CollectionViewModel
{
    public class MangaCollectionViewModel : ObservableCollection<MangaViewModel>, ISupportIncrementalLoading
    {
        #region Properties

        private MangaSource _mangaSource;
        private IDataService _dataService
        {
            get
            {
                return _mangaSource.DataService;
            }
        }
        private Category _category;
        private int _numberOfItemsPerLoading;

        private bool _isLoadingMore;
        public bool IsLoadingMore
        {
            get { return _isLoadingMore; }
            set
            {
                _isLoadingMore = value; OnPropertyChanged(new PropertyChangedEventArgs("IsLoadingMore"));
            }
        }

        #endregion
        public MangaCollectionViewModel()
        {

        }

        public MangaCollectionViewModel(MangaSource mangaSource, Category category, int numberOfItemsPerLoading)
        {
            _mangaSource = mangaSource;           
            _category = category;
            _numberOfItemsPerLoading = numberOfItemsPerLoading;
            HasMoreItems = true;
            IsLoadingMore = false;
        }

        public bool HasMoreItems
        {
            get;
            private set;
        }

        public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
        {
            return InnerLoadMoreItemsAsync(count).AsAsyncOperation();
        }

        private async Task<LoadMoreItemsResult> InnerLoadMoreItemsAsync(uint count)
        {
            int actualCount = 0;

            IsLoadingMore = true;

            List<Manga> mangaLoaded = null;
            try
            {
                //mangaLoaded = await _mangaSource.LoadMoreMangaAsync(_category, _numberOfItemsPerLoading);
                mangaLoaded = await _mangaSource.LoadMoreMangaFastAsync(_category, _numberOfItemsPerLoading);
            }
            catch(Exception ex)
            {
                Debug.WriteLine("Manga Collection View Model: " + ex.Message);
                HasMoreItems = false;
            }

            if(mangaLoaded != null && mangaLoaded.Any())
            {
                foreach (var manga in mangaLoaded)
                {
                    Add(new MangaViewModel(_mangaSource, manga));
                }
                actualCount += mangaLoaded.Count;
            }

            IsLoadingMore = false;

            return new LoadMoreItemsResult
            {
                Count = (uint)actualCount
            };
        }


    }
}
