using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyMovies.DAL;
using MyMovies.Helper;
using MyMovies.Model;
using static MyMovies.Model.Movie;

namespace MyMovies.Controllers
{
    // DTO inner class to help with Swagger documentation
    public class URLDTO
    {
        public String URL { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly MyMoviesContext _context;
        private IMovieRepository movieRepository;
        private readonly IMapper _mapper;

        public MoviesController(MyMoviesContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            this.movieRepository = new MovieRepository(new MyMoviesContext());
        }

        // GET: api/Movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovie()
        {
            return await _context.Movie.ToListAsync();
        }

        // GET: api/Movies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
            var movie = await _context.Movie.FindAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            return movie;
        }

        // PUT: api/Movies/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, Movie movie)
        {
            if (id != movie.MovieId)
            {
                return BadRequest();
            }

            _context.Entry(movie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Movies/
        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie([FromBody]URLDTO data)
        {
            Movie movie;
            String movieTitle;
            try
            {
                movieTitle = data.URL;
                // Constructing the video object from our helper function
                movie = MovieDbHelper.GetMovieInfo(movieTitle);
            }
            catch
            {
                return BadRequest("Cannot find requested movie");
            }

            // Add this video object to the database
            _context.Movie.Add(movie);
            await _context.SaveChangesAsync();

            // Return success code and the info on the movie object
            return CreatedAtAction("GetMovie", new { id = movie.MovieId }, movie);
        }

        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Movie>> DeleteMovie(int id)
        {
            var movie = await _context.Movie.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            _context.Movie.Remove(movie);
            await _context.SaveChangesAsync();

            return movie;
        }

        //PUT with PATCH to handle isFavourite
        [HttpPatch("update/{id}")]
        public MovieDTO Patch(int id, [FromBody]JsonPatchDocument<MovieDTO> moviePatch)
        {
            //get original movie object from the database
            Movie originMovie = movieRepository.GetMovieByID(id);
            //use automapper to map that to DTO object
            MovieDTO movieDTO = _mapper.Map<MovieDTO>(originMovie);
            //apply the patch to that DTO
            moviePatch.ApplyTo(movieDTO);
            //use automapper to map the DTO back ontop of the database object
            _mapper.Map(movieDTO, originMovie);
            //update video in the database
            _context.Update(originMovie);
            _context.SaveChanges();
            return movieDTO;
        }

        private bool MovieExists(int id)
        {
            return _context.Movie.Any(e => e.MovieId == id);
        }
    }
}
