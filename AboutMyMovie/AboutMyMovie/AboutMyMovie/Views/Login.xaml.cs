using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AboutMyMovie.ViewModels;

namespace AboutMyMovie.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {        
        public Login()
        {            
            InitializeComponent();
            BindingContext = new LoginViewModel();
        }
    }
}