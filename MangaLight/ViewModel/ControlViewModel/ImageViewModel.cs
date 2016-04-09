using GalaSoft.MvvmLight;
using MangaLight.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaLight.ViewModel.ControlViewModel
{
    public class ImageViewModel : ViewModelBase
    {
        #region Fields
        private ChapterImage _image;

        private bool _isLoadingImage = true;



        #endregion

        #region Properties

        public Uri ImageUri { get { return _image.ImageUri; } }
        public int ImageIndex { get { return _image.ImageIndex; } }

        public bool IsLoadingImage
        {
            get { return _isLoadingImage; }
            set { Set<bool>(ref _isLoadingImage, value, "IsLoadingImage"); }
        }

        #endregion
        public ImageViewModel(ChapterImage image)
        {
            _image = image;
        }


    }
}
