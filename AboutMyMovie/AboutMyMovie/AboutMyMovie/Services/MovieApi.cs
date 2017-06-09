using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AboutMyMovie.Interfaces;
using AboutMyMovie.Models;
using Newtonsoft.Json;

namespace AboutMyMovie.Services
{
    public class MovieApi : IMovieApi
    {
        private const string UrlBase = "http://www.omdbapi.com/";
        private const string ApiKey = "ae6969ba";
        private readonly HttpClient _httpClient;

        public MovieApi()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<Movie> GetMovieInfoAsync(string name)
        {
            var response = await _httpClient.GetAsync($"{UrlBase}?t={name}&apiKey={ApiKey}");
            if (response.IsSuccessStatusCode)
            {
                using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                {
                    return JsonConvert.DeserializeObject<Movie>(
                        await new StreamReader(responseStream)
                            .ReadToEndAsync().ConfigureAwait(false));
                }
            }
            return null;
        }
    }
}
