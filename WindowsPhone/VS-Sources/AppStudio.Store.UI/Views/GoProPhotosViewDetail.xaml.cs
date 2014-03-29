using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;

using AppStudio.Data;
using AppStudio.Common;

namespace AppStudio
{
    public partial class GoProPhotosViewDetail
    {
        private NavigationHelper _navigationHelper;

        private GoProPhotosViewModel _goProPhotosViewModel = null;

        public GoProPhotosViewDetail()
        {
            InitializeComponent();
            _navigationHelper = new NavigationHelper(this);
            DataContext = GoProPhotosViewModel;
        }

        public GoProPhotosViewModel GoProPhotosViewModel
        {
            get { return _goProPhotosViewModel ?? (_goProPhotosViewModel = GoProPhotosViewModel.Current); }
        }

        /// <summary>
        /// NavigationHelper is used on each page to aid in navigation and 
        /// process lifetime management
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this._navigationHelper; }
        }

        override protected async void OnNavigatedTo(NavigationEventArgs e)
        {
            await GoProPhotosViewModel.LoadItems(true);
            DataContext = GoProPhotosViewModel;
            base.OnNavigatedTo(e);
        }
    }
}
