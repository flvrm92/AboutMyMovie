using AboutMyMovie.Helpers;
using AboutMyMovie.Interfaces;
using AboutMyMovie.Services;
using AboutMyMovie.Views;
using Xamarin.Forms;

namespace AboutMyMovie.ViewModels
{
    public class MasterViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly AzureServices _azureServices;
        public string UserImageUrl => Settings.UserImageUrl;

        public string UserName => !string.IsNullOrEmpty(Settings.UserName) ? $"Welcome to About My Movie App,  {Settings.UserName}!" : string.Empty;

        public Command AboutCommand { get; set; }
        public Command SearchCommand { get; set; }
        public Command LogOutCommand { get; set; }

        public MasterViewModel()
        {
            _navigationService = DependencyService.Get<INavigationService>();
            _azureServices = DependencyService.Get<AzureServices>();

            SearchCommand = new Command(NavigateToSearchAsync);
            AboutCommand = new Command(NavigateToAboutAsync);
            LogOutCommand = new Command(LogOut);
        }

        private static async void NavigateToAboutAsync()
        {
            await App.NavigateMasterDetail(new About());
        }

        private static async void NavigateToSearchAsync()
        {
            await App.NavigateMasterDetail(new Search());
        }

        private async void LogOut()
        {
            if (!Settings.IsLoggedIn)
                return;

            if (await _azureServices.LogoutAsync())
                _navigationService.ToLogin();
        }

    }
}
