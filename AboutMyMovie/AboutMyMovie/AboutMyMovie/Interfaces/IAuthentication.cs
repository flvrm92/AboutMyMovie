using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;

namespace AboutMyMovie.Interfaces
{
    public interface IAuthentication
    {
        Task<MobileServiceUser> Authentication(MobileServiceClient client, MobileServiceAuthenticationProvider provider);

        Task<bool> LogoutAsync(MobileServiceClient client);
    }
}
