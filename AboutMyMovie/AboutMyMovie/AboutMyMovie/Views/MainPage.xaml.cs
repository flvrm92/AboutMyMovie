using Xamarin.Forms;

namespace AboutMyMovie.Views
{
    public partial class MainPage : MasterDetailPage
    {
        public MainPage()
        {
            InitializeComponent();
            Master = new Master();
            Detail = new NavigationPage(new Detail());

            App.MasterDetailPage = this;
        }
    }
}
