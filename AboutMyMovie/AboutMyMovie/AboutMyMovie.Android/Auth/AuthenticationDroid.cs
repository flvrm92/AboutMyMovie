using System.Threading.Tasks;
using AboutMyMovie.Droid.Auth;
using AboutMyMovie.Helpers;
using AboutMyMovie.Interfaces;
using Microsoft.WindowsAzure.MobileServices;
using Android.Webkit;

[assembly: Xamarin.Forms.Dependency(typeof(AuthenticationDroid))]
namespace AboutMyMovie.Droid.Auth
{
    public class AuthenticationDroid : IAuthentication
    {
        public async Task<MobileServiceUser> Authentication(MobileServiceClient client, MobileServiceAuthenticationProvider provider)
        {
            try
            {
                var user = await client.LoginAsync(Xamarin.Forms.Forms.Context, provider);

                Settings.AuthToken = user?.MobileServiceAuthenticationToken ?? string.Empty;
                Settings.UserId = user?.UserId;

                return user;
            }
            catch
            {
                return null;
            }
        }
        public async Task<bool> LogoutAsync(MobileServiceClient client)
        {
            try
            {
                CookieManager.Instance.RemoveAllCookie();
                await client.LogoutAsync();

                Settings.AuthToken = string.Empty;
                Settings.UserId = string.Empty;

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}