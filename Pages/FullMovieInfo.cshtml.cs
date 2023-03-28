using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Movieshake_Api.Pages.Shared;
using System.Diagnostics;

namespace Movieshake_Api.Pages
{
    public class FullMovieInfoModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string? ImdbID { get; set; }



        public MovieContentResponse? MovieResponse { get; set; }
        public async Task OnGetAsync()
        {
            var client = new HttpClient();
            var requestUri = $"http://www.omdbapi.com/?apikey=5860fd57&i={ImdbID}";
            var getResponse = await client.GetAsync(requestUri);
            getResponse.EnsureSuccessStatusCode();
            var contentResponse = await getResponse.Content.ReadAsStringAsync();
            MovieResponse = JsonConvert.DeserializeObject<MovieContentResponse>(contentResponse);
        }
    }
}
public class MovieContentResponse
{
    public string Title { get; set; }
    public string Released { get; set; }
    public string Runtime { get; set; }
    public string Actors { get; set; }
    public string Genre { get; set; }
    public string Plot { get; set; }
    public string language { get; set; }
    public Uri Poster { get; set; }
    public string imdbID { get; set; }
    public string Type { get; set; }
    public string totalSeasons { get; set; }
    
}








//public class MovieId
//{
//    public <imdbId> id{get; set;}
//}
//create a method called id and set the property to the imdbID 
//create another class of properties for the id response
//try and do this page like the index page but it should show only one film


//TRASH BIN =============================================================================================

//public MovieInfo? Movie { get; set; }