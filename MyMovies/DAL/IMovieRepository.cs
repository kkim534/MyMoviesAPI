using System;
using System.Collections.Generic;
using MyMovies.Model;

namespace MyMovies.DAL
{
    public interface IMovieRepository : IDisposable
    {
        IEnumerable<Movie> GetMovie();
        Movie GetMovieByID(int MovieId);
        void InsertMovie(Movie movie);
        void DeleteMovie(int MovieId);
        void UpdateMovie(Movie movie);
        void Save();
    }
}