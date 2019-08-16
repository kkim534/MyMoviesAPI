using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyMovies.Model;

namespace MyMovies.DAL
{
    public class MovieRepository : IMovieRepository, IDisposable
    {
        private MyMoviesContext context;

        public MovieRepository(MyMoviesContext context)
        {
            this.context = context;
        }

        public IEnumerable<Movie> GetMovie()
        {
            return context.Movie.ToList();
        }

        public Movie GetMovieByID(int id)
        {
            return context.Movie.Find(id);
        }

        public void InsertMovie(Movie movie)
        {
            context.Movie.Add(movie);
        }

        public void DeleteMovie(int movieId)
        {
            Movie movie = context.Movie.Find(movieId);
            context.Movie.Remove(movie);
        }

        public void UpdateMovie(Movie movie)
        {
            context.Entry(movie).State = EntityState.Modified;
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}