using AboutMyMovie.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AboutMyMovie.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Search : ContentPage
    {
        public Search()
        {
            InitializeComponent();
            BindingContext = new SearchViewModel();
        }
    }
}