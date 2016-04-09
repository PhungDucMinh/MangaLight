using MangaLight.CustomException;
using MangaLight.Model;
using MangaLight.ViewModel.ControlViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace MangaLight.ViewModel
{
    public class ChapterReadingViewModel
    {
        private ChapterViewModel _chapterViewModel;
        private MangaSource _mangaSource;
        private ObservableCollection<ImageViewModel> _imageCollection;
        public ObservableCollection<ImageViewModel> ImageCollection
        {
            get { return _imageCollection; }
            set { _imageCollection = value; }
        }

        public ChapterReadingViewModel(ChapterViewModel chapterViewModel)
        {
            _chapterViewModel = chapterViewModel;
            _mangaSource = chapterViewModel.MangaSource;
            ImageCollection = new ObservableCollection<ImageViewModel>();
            //LoadImageAsync();
        }

        public async void LoadImageAsync()
        {
            try
            {
                List<ChapterImage> loadedImages = await _mangaSource.LoadChapterImageAsync(_chapterViewModel);
                foreach (var image in loadedImages)
                {
                    ImageCollection.Add(new ImageViewModel(image));
                }
            }
            catch (MangaOutOfSourceException ex)
            {
                MessageDialog dialog = new MessageDialog("We can't load manga images!");
                dialog.Commands.Add(new UICommand("OK"));
                await dialog.ShowAsync();
            }
            catch (Exception ex)
            {
                MessageDialog dialog = new MessageDialog(ex.Message);
                dialog.Commands.Add(new UICommand("OK"));
                await dialog.ShowAsync();
            }
        }
    }
}
