using System.Threading.Tasks;

namespace AboutMyMovie.Interfaces
{
    public interface INavigationService
    {
        void ToLogin();
        Task ToMainView();
        Task ToAbout();
        Task ToSearch();
    }
}
