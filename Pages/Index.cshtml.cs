using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Movieshake_Api.Pages.Shared;
using Microsoft.Extensions.Logging;


namespace Movieshake_Api.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        [BindProperty(SupportsGet = true)]
        public string? SearchMovie { get; set; }
        public List<MovieInfo>? movies { get; set; }

        public async Task OnGetAsync()
        {
            try
            {
                var client = new HttpClient();
                var requestUri = $"http://www.omdbapi.com/?apikey=5860fd57&s={ SearchMovie ?? "avengers"}";
                var getResponse = await client.GetAsync(requestUri);
                getResponse.EnsureSuccessStatusCode();
                var contentResponse = await getResponse.Content.ReadAsStringAsync();
                var deserialMovie = JsonConvert.DeserializeObject<movieInfoResponse>(contentResponse);
                movies = deserialMovie?.Search;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching movie data.");
            }
        }

        public class movieInfoResponse
        {
            public List<MovieInfo>? Search { get; set; }
        }


    }
}
