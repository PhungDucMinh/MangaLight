using MangaLight.ViewModel;
using MangaLight.ViewModel.ControlViewModel;
using System;
using System.Collections.Generic;
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
    public sealed partial class MangaDetailPage : Page
    {
        //MangaDetailViewModel _vm = null;
        public MangaDetailPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Enabled;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                var mangaViewModel = e.Parameter as MangaViewModel;
                var currentViewModel =
                    new MangaDetailViewModel(mangaViewModel.Manga, mangaViewModel.MangaSource);

                var previousViewModel = DataContext as MangaDetailViewModel;
                if (!currentViewModel.isTheSameManga(previousViewModel))
                    this.DataContext = currentViewModel;

            }
        }

        private void NavigateBackToMainPage(object sender, TappedRoutedEventArgs e)
        {
            Navigation.NavigationQuick.NavigationBack();
        }

        private void NavigateToChapterReadingPage(object sender, ItemClickEventArgs e)
        {
            var currentFrame = Window.Current.Content as Frame;

            var chapterViewModel = e.ClickedItem  as ChapterViewModel;
            chapterViewModel.MangaSource = (DataContext as MangaDetailViewModel).MangaSource;
            currentFrame.Navigate(typeof(ChapterReadingPage), chapterViewModel);
        }
    }
}
