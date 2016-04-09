using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace MangaLight.View.Control
{

    public sealed partial class ImageControl : UserControl
    {


        public Stretch ImageStretch
        {
            get { return (Stretch)GetValue(ImageTretchProperty); }
            set { SetValue(ImageTretchProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ImageStretch.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageTretchProperty =
            DependencyProperty.Register("ImageStretch", typeof(Stretch), typeof(ImageControl), new PropertyMetadata(Stretch.Uniform, ImageStretchPropertyCallback));

        private static void ImageStretchPropertyCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ImageControl;
            if (e.NewValue.ToString() == "UniformToFill")
            {
                control.image.Stretch = Stretch.UniformToFill;
                
                //control.scrolViewer.VerticalScrollMode = ScrollMode.Enabled;
            }
            else if(e.NewValue.ToString() == "Uniform")
            {
                control.image.Stretch = Stretch.Uniform;
                //control.scrolViewer.VerticalScrollMode = ScrollMode.Disabled;
            }
            else if (e.NewValue.ToString() == "None")
            {
                control.image.Stretch = Stretch.None;
                //control.scrolViewer.VerticalScrollMode = ScrollMode.Disabled;
            }
            else if (e.NewValue.ToString() == "Fill")
            {
                control.image.Stretch = Stretch.Fill;
                //control.scrolViewer.VerticalScrollMode = ScrollMode.Disabled;
            }
        }

        public ImageControl()
        {
            this.InitializeComponent();
        }
    }
}
