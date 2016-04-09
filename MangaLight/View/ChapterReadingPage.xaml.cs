using MangaLight.ViewModel;
using MangaLight.ViewModel.ControlViewModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MangaLight.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ChapterReadingPage : Page
    {
        public ChapterReadingPage()
        {
            this.InitializeComponent();
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var chapterViewModel = (e.Parameter as ChapterViewModel);
            var vm = new ChapterReadingViewModel(chapterViewModel);            
            DataContext = vm;
            vm.LoadImageAsync();
        }

        private void Flipview_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            var template1 = Resources["ImageTemplate1"] as DataTemplate;
            var template2 = Resources["ImageTemplate2"] as DataTemplate;
            if (imageFlipView.ItemTemplate == template2)
            {
                imageFlipView.ItemTemplate = template1;
            }
            else
            {
                imageFlipView.ItemTemplate = template2;
            }
           
        }

        private void NavigationBackToMangaDetailPage(object sender, TappedRoutedEventArgs e)
        {
            Navigation.NavigationQuick.NavigationBack();
        }

        private void ImageOpen(object sender, RoutedEventArgs e)
        {
            var image = sender as Image;
            var dataContext = image.DataContext as ImageViewModel;
            dataContext.IsLoadingImage = false;
        }
    }
}
