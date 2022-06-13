using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.BL.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KorisnikController : ControllerBase
    {
        private readonly ModelContext _context;

        public KorisnikController(ModelContext context)
        {
            _context = context;
        }

        // GET: api/Korisnik
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AUser>>> GetAUsers()
        {
            if (_context.AUsers == null)
            {
                return NotFound();
            }

            var retVal = await _context.AUsers.ToListAsync();
            return retVal;
        }

        // GET: api/Korisnik/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AUser>> GetAUser(int id)
        {
            if (_context.AUsers == null)
            {
                return NotFound();
            }
            var aUser = await _context.AUsers.FindAsync(id);

            if (aUser == null)
            {
                return NotFound();
            }

            return aUser;
        }

        // PUT: api/Korisnik/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAUser(int id, AUser aUser)
        {
            if (id != aUser.Id)
            {
                return BadRequest();
            }

            _context.Entry(aUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AUserExists(id))
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

        // POST: api/Korisnik
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AUser>> PostAUser(AUser aUser)
        {
            if (_context.AUsers == null)
            {
                return Problem("Entity set 'ModelContext.AUsers'  is null.");
            }
            _context.AUsers.Add(aUser);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AUserExists(aUser.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAUser", new { id = aUser.Id }, aUser);
        }

        // DELETE: api/Korisnik/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAUser(int id)
        {
            if (_context.AUsers == null)
            {
                return NotFound();
            }
            var aUser = await _context.AUsers.FindAsync(id);
            if (aUser == null)
            {
                return NotFound();
            }

            _context.AUsers.Remove(aUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AUserExists(int id)
        {
            return (_context.AUsers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
