using MangaLight.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace MangaLight.Navigation
{
    public class NavigationQuick
    {
        static public void NavigationBack()
        {
            var currentFrame = Window.Current.Content as Frame;
            if (currentFrame.Content.GetType() == typeof(MangaDetailPage))
            {
                currentFrame.Navigate(typeof(MainPage));
            }
            else if (currentFrame.Content.GetType() == typeof(ChapterReadingPage))
            {
                currentFrame.Navigate(typeof(MangaDetailPage));
            }
        } 
    }
}
