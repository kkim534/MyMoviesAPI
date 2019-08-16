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
            Console.WriteLine(GetVideoIdFromURL("https://www.youtube.com/watch?v=ehvz3iN8pp4"));

            // Pause the program execution
            Console.ReadLine();
        }

        public static Movie GetMovieInfo(String movieTitle)
        {
            String APIKey = "a69968833a9423d6c8c3179f51374223";
            String movieDbAPIURL = "https://api.themoviedb.org/3/search/movie?api_key=" + APIKey + "&query=" + movieTitle;
            // Use an http client to grab the JSON string from the web.
            String movieInfoJSON = new WebClient().DownloadString(movieDbAPIURL);

            // Using dynamic object helps us to more efficiently extract information from a large JSON String.
            dynamic jsonObj = JsonConvert.DeserializeObject<dynamic>(movieInfoJSON);
            return videoURL;
        }
    }
}
