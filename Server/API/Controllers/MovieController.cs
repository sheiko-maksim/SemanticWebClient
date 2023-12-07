using API.Services;
using API.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly MovieService movieService;
        private readonly IConfiguration configuration;

        public MovieController(MovieService movieService, IConfiguration configuration)
        {
            this.movieService = movieService;
            this.configuration = configuration;
        }

        [HttpGet("movies")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<MovieResponse>> GetAllMovies()
        {

            var result = await this.movieService.GetMovies();
            if (result.Count() > 0)
            {
                return this.Ok(result);
            }

            return this.BadRequest("Something is wrong");
        }


        [HttpGet("shows")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<MovieResponse>> GetAllShows()
        {

            var result = await this.movieService.GetTVShows();
            if (result.Count() > 0)
            {
                return this.Ok(result);
            }

            return this.BadRequest("Something is wrong");
        }


        [HttpGet("getOne")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<MovieResponse>> GetOne(Guid id)
        {

            var result = await this.movieService.GetOne(id);
            if (result.Title != String.Empty)
            {
                return this.Ok(result);
            }

            return this.BadRequest("Something is wrong");
        }
    }
}
