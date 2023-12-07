using API.ViewModels;
using DataAccess.Repositories.Realization;

namespace API.Services
{
    public class MovieService
    {
        private readonly IConfiguration config;
        private readonly ILogger logger;
        private readonly MovieRepository repository;

        public MovieService(IConfiguration config, ILogger<MovieService> logger, MovieRepository repository)
        {
            this.config = config;
            this.logger = logger;
            this.repository = repository;
        }

        public async Task<IEnumerable<MovieResponse>> GetMovies()
        {
            var movies = await this.repository.FindAsync(x => x.MovieType.Id == 1);
            var response = new List<MovieResponse>();
            foreach (var item in movies)
            {
                var movie = new MovieResponse
                {
                    Id = item.Id,
                    Title = item.Title,
                    Director = item.Director,
                    PublicationDate = item.PublicationDate,
                    Genres = item.Genres,
                    Topics = item.Topics,
                    Imdb = item.Imdb,
                };
                response.Add(movie);
            }
            return response;
        }

        public async Task<IEnumerable<MovieResponse>> GetTVShows()
        {
            var movies = await this.repository.FindAsync(x => x.MovieType.Id == 2);
            var response = new List<MovieResponse>();
            foreach (var item in movies)
            {
                var movie = new MovieResponse
                {
                    Id = item.Id,
                    Title = item.Title,
                    Director = item.Director,
                    PublicationDate = item.PublicationDate,
                    Genres = item.Genres,
                    Topics = item.Topics,
                    Imdb = item.Imdb,
                };
                response.Add(movie);
            }
            return response;
        }

        public async Task<MovieResponse> GetOne(Guid id)
        {
            var items = await this.repository.FindAsync(x => x.Id == id);
            var item = items.FirstOrDefault();
            var movie = new MovieResponse
            {
                Id = item.Id,
                Title = item.Title,
                Director = item.Director,
                PublicationDate = item.PublicationDate,
                Genres = item.Genres,
                Topics = item.Topics,
                Imdb = item.Imdb,
            };
            return movie;
        }
    }
}
