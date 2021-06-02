using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieCharFra.Models;
using MovieCharFra.Models.DTOs.Movie;

namespace MovieCharFra.Controllers
{
    [Route("api/v1/movie")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]

    public class MovieController : ControllerBase
    {
        private readonly MovieCharFraDbContext _context;
        private readonly IMapper _mapper;

        public MovieController(MovieCharFraDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        /// <summary>
        /// Get all the movies in the database
        /// </summary>
        /// <returns></returns>
        // GET: api/Movie
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieReadDTO>>> GetMovies()
        {
            return _mapper.Map<List<MovieReadDTO>>(await _context.Movies.Include(m => m.Characters).Include(m => m.Franchise).ToListAsync());
        }
        /// <summary>
        /// Get specific movie by its ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Movie/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MovieReadDTO>> GetMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            return _mapper.Map <MovieReadDTO> (movie);
        }

        /// <summary>
        /// Updates a movie.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="movie"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, Movie movie)
        {
            if (id != movie.Id)
            {
                return BadRequest();
            }
            //Movie domainMovie = _mapper.Map<Movie>(movie);
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

        /// <summary>
        ///  Adds a new movie to the database.
        /// </summary>
        /// <param name="movie"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(Movie movie)
        {
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMovie", new { id = movie.Id }, movie);
        }

        /// <summary>
        /// Deletes a movie.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }

        /// <summary>
        /// Get only characters for movie
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/Characters")]
        public async Task<ActionResult<IEnumerable<Character>>> GetCharsForMovie(int id)
        {
            Movie Movie = await _context.Movies.Include(c => c.Characters).Where(c => c.Id == id).FirstOrDefaultAsync();
            if (Movie == null)
            {
                return NotFound();
            }
            foreach (Character Character in Movie.Characters)
            {
                Character.Movies = null;
            }

            return Movie.Characters.ToList();
        }

        /// <summary>
        /// Add only characters to a movie
        /// </summary>
        /// <param name="id"></param>
        /// <param name="Characters"></param>
        /// <returns></returns>
        [HttpPost("{id}/Characters")]
        public async Task<IActionResult> AddCharsToMovie(int id, List<int> Characters)
        {
            Movie Movie = await _context.Movies.Include(c => c.Characters).FirstOrDefaultAsync(c => c.Id == id);
            if (Movie == null)
            {
                return NotFound();
            }

            foreach (int charid in Characters)
            {
                if (Movie.Characters.FirstOrDefault(c => c.Id == charid) == null)
                {
                    Character chara = await _context.Characters.FindAsync(charid);
                    if (chara != null)
                    {
                        Movie.Characters.Add(chara);
                    }
                }
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }
        /// <summary>
        /// Delete given characters for movie
        /// </summary>
        /// <param name="id"></param>
        /// <param name="Characters"></param>
        /// <returns></returns>
        [HttpDelete("{id}/Characters")]
        public async Task<IActionResult> DeleteCharsForMovie(int id, List<int> Characters)
        {
            Movie movie = await _context.Movies.Include(c => c.Characters).FirstOrDefaultAsync(c => c.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            //movie.Characters.Clear();

            foreach (int charid in Characters)
            {
                Character chara = await _context.Characters.FindAsync(charid);
                movie.Characters.Remove(chara);
                
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
