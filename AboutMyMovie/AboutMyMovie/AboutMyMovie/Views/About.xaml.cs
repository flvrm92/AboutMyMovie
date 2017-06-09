using AboutMyMovie.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AboutMyMovie.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class About : ContentPage
    {
        public About()
        {
            InitializeComponent();
            BindingContext = new AboutViewModel();
        }
    }
}