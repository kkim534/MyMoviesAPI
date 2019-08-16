using AutoMapper;
using static MyMovies.Model.Movie;

namespace MyMovies.Model
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Movie, MovieDTO>();
            CreateMap<MovieDTO, Movie>();
        }
    }
}