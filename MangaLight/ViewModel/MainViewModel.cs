using GalaSoft.MvvmLight;
using MangaLight.Model;
using MangaLight.Service;
using MangaLight.Service.TruyenTranhTuan;
using MangaLight.ViewModel.CollectionViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaLight.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        #region Fields
        private MangaSource _mangaSource;

        private MangaCollectionViewModel _mostPopularMangaCollection;

        private MangaCollectionViewModel _latestUpdatedMangaCollection;

        private List<string> _allMangaName;



        #endregion

        #region Properties

        public MangaSource MangaSource
        {
            get { return _mangaSource; }
            set { _mangaSource = value; }
        }

        
        public MangaCollectionViewModel MostPopularMangaCollection
        {
            get { return _mostPopularMangaCollection; }
            set { _mostPopularMangaCollection = value; }
        }
        
        public MangaCollectionViewModel LatestUpdatedMangaCollection
        {
            get { return _latestUpdatedMangaCollection; }
            set { _latestUpdatedMangaCollection = value; }
        }

        public List<string> AllMangaName
        {
            get { return _allMangaName; }
            set { _allMangaName = value; }
        }

        #endregion

        public MainViewModel()
        {
            _mangaSource = new MangaSource(new TruyenTranhTuanDataService());
            _mostPopularMangaCollection = new MangaCollectionViewModel(_mangaSource, Category.All, 50);
            _latestUpdatedMangaCollection = new MangaCollectionViewModel(_mangaSource, Category.LatestUpdated, 20);
            
        }

        public MainViewModel(MangaSource mangaSource)
        {
            _mangaSource = mangaSource;
            _mostPopularMangaCollection = new MangaCollectionViewModel(_mangaSource, Category.All, 50);
            _latestUpdatedMangaCollection = new MangaCollectionViewModel(_mangaSource, Category.LatestUpdated, 20);
        }

        public List<Manga> GetMatchingMangaList(string query)
        {
            List<Manga> allManga = _mangaSource.GetAllManga();
            var matchingMangaList = allManga.FindAll(p => p.MangaInfo.MangaName.IndexOf(query, StringComparison.CurrentCultureIgnoreCase) >= 0);
            return matchingMangaList;
        }
    }
}
