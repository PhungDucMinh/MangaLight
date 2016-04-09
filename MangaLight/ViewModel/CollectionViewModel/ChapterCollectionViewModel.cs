using MangaLight.Model;
using MangaLight.ViewModel.ControlViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Data;

namespace MangaLight.ViewModel.CollectionViewModel
{
    public class ChapterCollectionViewModel : ObservableCollection<ChapterViewModel>, ISupportIncrementalLoading
    {
        #region Properties

        private MangaSource _mangaSource;
        private Manga _manga;
        private uint _numberOfChaptersPerLoading;
        #endregion


        public ChapterCollectionViewModel(MangaSource mangaSource, Manga manga)
        {
            _mangaSource = mangaSource;
            _manga = manga;
            HasMoreItems = true;
        }

        public ChapterCollectionViewModel(MangaSource mangaSource, Manga manga, uint numberOfChaptersPerLoading)
        {
            _mangaSource = mangaSource;
            _manga = manga;
            _numberOfChaptersPerLoading = numberOfChaptersPerLoading;
            HasMoreItems = true;
        }

        public bool HasMoreItems
        {
            get; private set;
        }

        public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
        {
            return InnerLoadMoreItemsAsync(count).AsAsyncOperation();
        }

        protected async Task<LoadMoreItemsResult> InnerLoadMoreItemsAsync(uint count)
        {
            List<Chapter> loadedChapters = null;
            try
            {
                loadedChapters = await _mangaSource.LoadMoreChapterAsync(_manga.MangaInfo.MangaUri ,count);
            }
            catch (Exception ex)
            {
                HasMoreItems = false;
                Debug.WriteLine("Chapter Collection View Model exception:", ex.Message);
            }

            if(loadedChapters != null && loadedChapters.Any())
            {
                foreach (var chapter in loadedChapters)
                {
                    Add(new ChapterViewModel(_mangaSource, _manga, chapter));
                }
            }
            return new LoadMoreItemsResult
            {
                Count = (uint)loadedChapters.Count,
            };
                            
        }
    }
}
