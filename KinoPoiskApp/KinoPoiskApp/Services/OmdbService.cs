using KinoPoiskApp.DTO;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace KinoPoiskApp.Services
{
    public class OmdbService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = "";

        public OmdbService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<MovieDto> GetMovieAsync(string title)
        {
            var url = $"http://www.omdbapi.com/?t={title}&apikey={_apiKey}&plot=short";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();
            var movie = JObject.Parse(json);

            if (movie["Response"]?.ToString() == "False")
                return null;

            return new MovieDto
            {
                Title = movie["Title"]?.ToString(),
                Year = movie["Year"]?.ToString(),
                Rating = movie["imdbRating"]?.ToString(),
                Runtime = movie["Runtime"]?.ToString(),
                Director = movie["Director"]?.ToString(),
                Actors = movie["Actors"]?.ToString(),
                Plot = movie["Plot"]?.ToString()
            };
        }
    }
}