using System.Threading.Tasks;
using AboutMyMovie.Helpers;
using AboutMyMovie.Interfaces;
using AboutMyMovie.Services;
using Xamarin.Forms;

namespace AboutMyMovie.Views
{
    public partial class App : Application
    {
        public static MasterDetailPage MasterDetailPage { get; set; }

        private static void RegisterDependencies()
        {
            DependencyService.Register<INavigationService, NavigationService>();
            DependencyService.Register<IMovieApi, MovieApi>();
        }

        public static async Task NavigateMasterDetail(Page page, bool isPresented = false)
        {
            MasterDetailPage.IsPresented = isPresented;
            await MasterDetailPage.Detail.Navigation.PushAsync(page);
        }        

        public App()
        {
            InitializeComponent();

            RegisterDependencies();

            if (Settings.IsLoggedIn)
                MainPage = new MainPage();
            else
                MainPage = new Login();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
