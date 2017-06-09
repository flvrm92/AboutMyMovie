using AboutMyMovie.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AboutMyMovie.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Master : ContentPage
    {
        public Master()
        {
            InitializeComponent();
            BindingContext = new MasterViewModel();
        }
    }
}