using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieCharFra.Models;

namespace MovieCharFra.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly MovieCharFraDbContext _context;

        public MovieController(MovieCharFraDbContext context)
        {
            _context = context;
        }

        // GET: api/Movie
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            return await _context.Movies.ToListAsync();
        }

        // GET: api/Movie/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            return movie;
        }

        // PUT: api/Movie/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, Movie movie)
        {
            if (id != movie.Id)
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

        // POST: api/Movie
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(Movie movie)
        {
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMovie", new { id = movie.Id }, movie);
        }

        // DELETE: api/Movie/5
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

        // 1. Get certifications for coach
        // 2. Add certifications to coach
        // 3. Update existing certifications for coach
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
        [HttpPut("{id}/Characters")]
        public async Task<IActionResult> UpdateCharsForMovie(int id, List<int> Characters)
        {
            Movie movie = await _context.Movies.Include(c => c.Characters).FirstOrDefaultAsync(c => c.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            movie.Characters.Clear();

            foreach (int charid in Characters)
            {
                Character chara = await _context.Characters.FindAsync(charid);
                movie.Characters.Add(chara);
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
