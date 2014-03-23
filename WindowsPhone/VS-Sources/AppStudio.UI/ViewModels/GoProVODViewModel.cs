using System;
using System.Windows;
using System.Windows.Input;

using AppStudio.Services;

namespace AppStudio.Data
{
    public class GoProVODViewModel : ViewModelBase<YouTubeSchema>
    {
        override protected string CacheKey
        {
            get { return "GoProVODDataSource"; }
        }

        override protected IDataSource<YouTubeSchema> CreateDataSource()
        {
            return new GoProVODDataSource(); // YouTubeDataSource
        }

        override public bool IsGoToSourceVisible
        {
            get { return ViewType == ViewTypes.Detail; }
        }
        
        override public void GoToSource()
        {
            base.GoToSource("{ExternalUrl}");
        }

        override public bool IsRefreshVisible
        {
            get { return ViewType == ViewTypes.List; }
        }

        override public void ImageTap(string url)
        {
            var currentItem = GetCurrentItem();
            if (currentItem != null)
            {
                MyToolkit.Multimedia.YouTube.Play(currentItem.VideoId, MyToolkit.Multimedia.YouTubeQuality.Quality480P, e =>
                {
                    if (e != null)
                        System.Windows.MessageBox.Show(e.Message);
                });
            }
        }
    }
}
