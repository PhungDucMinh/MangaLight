using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MangaLight.Service;
using Windows.Storage;
using System.IO;
using System.Diagnostics;
using System.Xml;
using GalaSoft.MvvmLight;
using MangaLight.Service.TruyenTranhTuan;
using MangaLight.ViewModel.ControlViewModel;

namespace MangaLight.Model
{
    public class MangaSource
    {
        #region Fields
        private IDataService _dataService;


        #endregion

        #region Properties

        public IDataService DataService
        {
            get { return _dataService; }
            set { _dataService = value; }
        }
        
        #endregion

        #region Constructor
        public MangaSource(IDataService dataService)
        {
            _dataService = dataService;
        }

        public MangaSource(TruyenTranhTuanDataService dataService)
        {
            _dataService = dataService;
        }

        public async Task<List<Manga>> LoadMoreMangaFastAsync(Category category, int count)
        {
            if (category == Category.All)
            {
                List<Manga> loadedManga = await _dataService.LoadMoreMangaFastAsync(category, count);
                return loadedManga;
            }
            else if (category == Category.MostPopular)
            {
                List<Manga> loadedManga = await _dataService.LoadMoreMangaFastAsync(category, count);
                return loadedManga;
            }
            else if (category == Category.LatestUpdated)
            {
                List<Manga> loadedManga = await _dataService.LoadMoreMangaFastAsync(category, count);
                return loadedManga;
            }
            return null;
        }



        #endregion


        #region Important Methods
        public async Task<List<Manga>> LoadMoreMangaAsync(Category category, int count)
        {
            if(category == Category.All)
            {
                List<Manga> loadedManga = await _dataService.LoadMoreMangaAsync(category, count);
                return loadedManga;
            }
            else if(category == Category.MostPopular)
            {
                List<Manga> loadedManga = await _dataService.LoadMoreMangaAsync(category, count);
                return loadedManga;
            }
            else if (category == Category.LatestUpdated)
            {
                List<Manga> loadedManga = await _dataService.LoadMoreMangaAsync(category, count);
                return loadedManga;
            }
            return null;
        }

        public List<Manga> GetAllManga()
        {
            return _dataService.AllManga;
        }

        public async Task<Uri> GetMangaImageUri(Manga manga)
        {
            return await _dataService.GetMangaCoverUri(manga);
        }

        public async Task<List<Chapter>> LoadMoreChapterAsync(Uri mangaUri, uint count)
        {
            return await _dataService.LoadMoreChapterAsync(mangaUri, count);
        }

        public async Task<List<ChapterImage>> LoadChapterImageAsync(ChapterViewModel chapterViewModel)
        {
            return await _dataService.LoadChapterImageAsync(chapterViewModel.ChapterUri);
        }

        public async Task<bool> DownloadMangaWithLocalFolderAsync(Manga manga)
        {
            return await _dataService.DownloadMangaWithLocalFolderAsync(manga);
        }

        public async Task<bool> DownloadMangaWithLocalFolderAsync(Manga manga, List<Chapter> chapters)
        {
            return await _dataService.DownloadMangaWithLocalFolderAsync(manga, chapters);
        }

        public async Task<bool> DownloadMangaWithSpecifyFolderAsync(Manga manga)
        {
            return await _dataService.DownloadMangaWithSpecifyFolderAsync(manga);
        }

        public async Task<bool> DownloadMangaWithSpecifyFolderAsync(Manga manga, List<Chapter> chapters)
        {
            return await _dataService.DownloadMangaWithSpecifyFolderAsync(manga, chapters);
        }

        public Task<bool> DownloadChapterAsync(Chapter _chapter)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
