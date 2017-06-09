using System.Threading.Tasks;
using AboutMyMovie.Helpers;
using AboutMyMovie.Interfaces;
using AboutMyMovie.Services;
using Xamarin.Forms;

namespace AboutMyMovie.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly AzureServices _azureService;
        public Command LoginCommand { get; }

        public LoginViewModel()
        {
            _azureService = DependencyService.Get<AzureServices>();
            _navigationService = DependencyService.Get<INavigationService>();
            LoginCommand = new Command(ExecuteLoginAsync);
        }

        private async void ExecuteLoginAsync()
        {
            if (!(await LoginAsync()))
                return;

            await _navigationService.ToMainView();
        }

        public Task<bool> LoginAsync()
        {
            if (Settings.IsLoggedIn)
                return Task.FromResult(true);

            return _azureService.LoginAsync();
        }
    }
}
