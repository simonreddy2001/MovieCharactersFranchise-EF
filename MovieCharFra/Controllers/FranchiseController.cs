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
using MovieCharFra.Models.DTOs.Franchise;

namespace MovieCharFra.Controllers
{
    [Route("api/v1/franchise")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]

    public class FranchiseController : ControllerBase
    {
        private readonly MovieCharFraDbContext _context;
        private readonly IMapper _mapper;

        public FranchiseController(MovieCharFraDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all the franchises in the database
        /// </summary>
        /// <returns></returns>
        // GET: api/Franchise
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FranchiseReadDTO>>> GetFranchises()
        {
            return _mapper.Map<List<FranchiseReadDTO>>(await _context.Franchises.Include(f => f.Movies).ToListAsync());
        }

        /// <summary>
        /// Get specific franchise by its ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Franchise/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FranchiseReadDTO>> GetFranchise(int id)
        {
            var franchise = await _context.Franchises.FindAsync(id);

            if (franchise == null)
            {
                return NotFound();
            }

            return _mapper.Map<FranchiseReadDTO>(franchise);
        }

       /// <summary>
       /// Updates a franchise
       /// </summary>
       /// <param name="id"></param>
       /// <param name="franchise"></param>
       /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFranchise(int id, FranchiseEditDTO franchise)
        {
            if (id != franchise.Id)
            {
                return BadRequest();
            }
            Franchise domainFranchise = _mapper.Map<Franchise>(franchise);
            _context.Entry(domainFranchise).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FranchiseExists(id))
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
        /// Adds a new franchise to the database
        /// </summary>
        /// <param name="franchise"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Franchise>> PostFranchise(Franchise franchise)
        {
            _context.Franchises.Add(franchise);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFranchise", new { id = franchise.Id }, franchise);
        }

        /// <summary>
        /// Deletes a franchise
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFranchise(int id)
        {
            var franchise = await _context.Franchises.FindAsync(id);
            if (franchise == null)
            {
                return NotFound();
            }

            _context.Franchises.Remove(franchise);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FranchiseExists(int id)
        {
            return _context.Franchises.Any(e => e.Id == id);
        }

        /// <summary>
        /// Get only movies in a franchise
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/Movies")]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMoviesForFranchise(int id)
        {
            Franchise Franchise = await _context.Franchises.Include(f => f.Movies).Where(f => f.Id == id).FirstOrDefaultAsync();
            if (Franchise == null)
            {
                return NotFound();
            }
            foreach (Movie Movie in Franchise.Movies)
            {
                Movie.Franchise = null;
            }

            return Franchise.Movies.ToList();
        }

        /// <summary>
        /// Add only movies to a franchise
        /// </summary>
        /// <param name="id"></param>
        /// <param name="Movies"></param>
        /// <returns></returns>
        [HttpPost("{id}/Movies")]
        public async Task<IActionResult> AddMoviesToFranchise(int id, List<int> Movies)
        {
            Franchise Franchise = await _context.Franchises.Include(c => c.Movies).FirstOrDefaultAsync(c => c.Id == id);
            if (Franchise == null)
            {
                return NotFound();
            }

            foreach (int movieid in Movies)
            {
                if (Franchise.Movies.FirstOrDefault(c => c.Id == movieid) == null)
                {
                    Movie mov = await _context.Movies.FindAsync(movieid);
                    if (mov != null)
                    {
                        Franchise.Movies.Add(mov);
                    }
                }
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }
        /// <summary>
        /// Delete given movies for franchise
        /// </summary>
        /// <param name="id"></param>
        /// <param name="Movies"></param>
        /// <returns></returns>
        [HttpDelete("{id}/Movies")]
        public async Task<IActionResult> UpdateMoviesForFranchise(int id, List<int> Movies)
        {
            Franchise Franchise = await _context.Franchises.Include(c => c.Movies).FirstOrDefaultAsync(c => c.Id == id);
            if (Franchise == null)
            {
                return NotFound();
            }

            foreach (int movieid in Movies)
            {
                Movie mov = await _context.Movies.FindAsync(movieid);
                Franchise.Movies.Remove(mov);
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Get all characters by their franchise
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/Characters")]
        public async Task<ActionResult<IEnumerable<Franchise>>> GetCharactersForFranchise(int id)
        {
            var FranchiseChar = await _context.Franchises.Include(f => f.Movies)
                .ThenInclude(m=>m.Characters).ThenInclude(mc=>mc.Id)
                .Where(c => c.Id == id).ToListAsync();
            if (FranchiseChar == null)
            {
                return NotFound();
            }

            return FranchiseChar.ToList();
        }
    }
}
