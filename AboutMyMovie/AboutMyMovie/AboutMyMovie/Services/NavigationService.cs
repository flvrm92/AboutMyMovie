using System.Threading.Tasks;
using AboutMyMovie.Interfaces;
using AboutMyMovie.Views;
using Xamarin.Forms;

namespace AboutMyMovie.Services
{
    public class NavigationService : INavigationService
    {
        public void ToLogin()
        {
            Application.Current.MainPage = new Login();
        }

        public async Task ToMainView()
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new MainPage());
        }

        public async Task ToAbout()
        {
            await App.NavigateMasterDetail(new About());
        }

        public async Task ToSearch()
        {
            await App.NavigateMasterDetail(new Search());
        }
    }
}
