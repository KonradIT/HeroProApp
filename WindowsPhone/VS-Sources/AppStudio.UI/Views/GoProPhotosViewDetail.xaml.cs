using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows.Threading;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Net.NetworkInformation;

using Microsoft.Phone.Controls;

using MyToolkit.Paging;

using AppStudio.Data;
using AppStudio.Services;

namespace AppStudio
{
    public partial class GoProPhotosViewDetail
    {
        private bool _isDeepLink = false;

        public GoProPhotosViewDetail()
        {
            InitializeComponent();
        }

        public GoProPhotosViewModel GoProPhotosViewModel { get; private set; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            GoProPhotosViewModel = NavigationServices.CurrentViewModel as GoProPhotosViewModel;
            if (e.NavigationMode == NavigationMode.New && NavigationContext.QueryString.ContainsKey("id"))
            {
                string id = NavigationContext.QueryString["id"];
                if (!String.IsNullOrEmpty(id))
                {
                    _isDeepLink = true;
                    GoProPhotosViewModel = new GoProPhotosViewModel();
                    NavigationServices.CurrentViewModel = GoProPhotosViewModel;
                    GoProPhotosViewModel.LoadItem(id);
                }
            }
            if (GoProPhotosViewModel != null)
            {
                GoProPhotosViewModel.ViewType = ViewTypes.Detail;
            }
            DataContext = GoProPhotosViewModel;
            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.Back)
            {
                NavigationServices.CurrentViewModel = null;
            }
            SpeechServices.Stop();
            base.OnNavigatedFrom(e);
        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            if (_isDeepLink)
            {
                _isDeepLink = false;
                NavigationServices.NavigateToPage("MainPage");
            }
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SpeechServices.Stop();
        }
    }
}
