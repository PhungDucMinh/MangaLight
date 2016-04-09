using MangaLight.Model;
using MangaLight.Service;
using MangaLight.Service.TruyenTranhTuan;
using MangaLight.ViewModel;
using MangaLight.ViewModel.ControlViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MangaLight.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        IDataService _dataService = null;
        MangaSource _mangaSource = null;
        public MainPage()
        {            
            this.InitializeComponent();
            #region Temporaly delete

            //_dataService = new TruyenTranhTuanDataService();
            //_mangaSource = new MangaSource(_dataService);
            //MainViewModel _vm = new MainViewModel(_mangaSource);
            //DataContext = _vm;
            #endregion

            NavigationCacheMode = NavigationCacheMode.Enabled;
            var navigationManager = SystemNavigationManager.GetForCurrentView();
            navigationManager.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            navigationManager.BackRequested += NavigationManager_BackRequested;

            this.DataContextChanged += (s, e) => { MainViewModel = DataContext as MainViewModel; };
        }

        public MainViewModel MainViewModel { get; set; }

        private void NavigationManager_BackRequested(object sender, BackRequestedEventArgs e)
        {
            Navigation.NavigationQuick.NavigationBack();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            //(this.DataContext as MainViewModel).LoadAllMangaName();
        }

        private void MangaSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            var autoSuggestBox =sender as AutoSuggestBox;
            var query =autoSuggestBox.Text;
            if ( query.Count() >= 2)
            {
                List<Manga> matchingMangaList = viewModel.GetMatchingMangaList(query);
                autoSuggestBox.ItemsSource = matchingMangaList;
            }
        }

        private void MangaSuggestBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {

        }

        private void MangaSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {

        }

        private void NavigateToMangaClick(object sender, RoutedEventArgs e)
        {

        }

        private void NavigateToMangaDetailPage(object sender, TappedRoutedEventArgs e)
        {
            //Add MangaSource to MangaViewModel then pass to Navigation parameter.
            var mangaViewModel = (sender as FrameworkElement).DataContext as MangaViewModel;
            mangaViewModel.MangaSource = (DataContext as MainViewModel).MangaSource ;
            var frame = Window.Current.Content as Frame;
            
            frame.Navigate(typeof(MangaDetailPage), mangaViewModel);
        }
    }
}
