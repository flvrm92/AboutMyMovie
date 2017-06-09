using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AboutMyMovie.Helpers;
using AboutMyMovie.Interfaces;
using AboutMyMovie.Models;
using AboutMyMovie.Services;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(AzureServices))]
namespace AboutMyMovie.Services
{
    public class AzureServices
    {
        private const string AppUrl = "http://socialloginaboutmymovie.azurewebsites.net";
        public MobileServiceClient Client { get; set; }

        public AzureServices()
        {
            Initialize();
        }
        public void Initialize()
        {
            Client = new MobileServiceClient(AppUrl);
        }
        public async Task<bool> LogoutAsync()
        {
            var auth = DependencyService.Get<IAuthentication>();

            return await auth.LogoutAsync(Client);
        }

        public async Task<bool> LoginAsync()
        {
            var auth = DependencyService.Get<IAuthentication>();
            var user = await auth.Authentication(Client, MobileServiceAuthenticationProvider.Facebook);

            if (user == null)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await
                        Application.Current.MainPage.DisplayAlert("Opsss!", "Não conseguimos efetuar o login :/",
                            "Ok");
                });
                return false;
            }

            Settings.AuthToken = user.MobileServiceAuthenticationToken;
            Settings.UserId = user.UserId;

            await SetUserInfoAsyc();

            return true;
        }       

        public async Task SetUserInfoAsyc()
        {
            var identities = await Client.InvokeApiAsync<List<AppServiceIdentity>>("/.auth/me");

            if (identities.Count <= 0)
                return;

            var name = identities[0].UserClaims.Find(c => c.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname")).Value;
            var userToken = identities[0].AccessToken;

            var requestUrl = $"https://graph.facebook.com/v2.9/me/?fields=picture&access_token={userToken}";
            var httpClient = new HttpClient();

            var userJson = await httpClient.GetStringAsync(requestUrl);

            var facebookProfile = JsonConvert.DeserializeObject<FacebookProfile>(userJson);

            Settings.UserName = name;
            Settings.UserImageUrl = facebookProfile.Picture.Data.Url;            
        }
    }
}
