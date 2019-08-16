using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MyMovies.Model;
using Newtonsoft.Json;

namespace MyMovies.Helper
{
    public class MovieDbHelper
    {
        public static void testProgram()
        {
            GetMovieInfo("titanic");
        }

        public static Movie GetMovieInfo(String movieTitle)
        {
            String APIKey = "a69968833a9423d6c8c3179f51374223";
            String movieDbAPIURL = "https://api.themoviedb.org/3/search/movie?api_key=" + APIKey + "&query=" + movieTitle;

            // Use an http client to grab the JSON string from the web.
            String movieInfoJSON = new WebClient().DownloadString(movieDbAPIURL);

            // Using dynamic object helps us to more efficiently extract information from a large JSON String.
            dynamic jsonObj = JsonConvert.DeserializeObject<dynamic>(movieInfoJSON);
            String title = jsonObj["results"][0]["title"];
            String posterPath = jsonObj["results"][0]["poster_path"];
            String moviePlot = jsonObj["results"][0]["overview"];
            String movieId = jsonObj["results"][0]["id"];

            // API call for movie crew information
            String directorAPIURL = "https://api.themoviedb.org/3/movie/" + movieId + "?api_key=" + APIKey + "&append_to_response=credits";
            String directorInfoJSON = new WebClient().DownloadString(directorAPIURL);

            // Extract information from the dynamic object.
            dynamic jsonDir = JsonConvert.DeserializeObject<dynamic>(directorInfoJSON);
            String actor1 = jsonDir["credits"]["cast"][0]["name"];
            String actor2 = jsonDir["credits"]["cast"][1]["name"];

            // Iterate through crew and find director
            String movieDirector = "";
            foreach (var item in jsonDir["credits"]["crew"])
            {
                if (item["job"] == "Director")
                {
                    movieDirector = item["name"];
                }      
            }

            Movie movie = new Movie
            {
                MovieTitle = title,
                MovieDbId = Int32.Parse(movieId),
                ThumbnailUrl = posterPath,
                Plot = moviePlot,
                Cast = actor1 + " and " + actor2,
                Director = movieDirector,
                IsFavourite = false
            };
            String test = title + " " + moviePlot + " " + movieDirector + " " + actor1 + " " + actor2;
            return movie;
        }
    }
}
