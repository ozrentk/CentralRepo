using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.BL.Models;
using WebAPI.BL.Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class KorisnikController : ControllerBase
    {
        private readonly ModelContext _context;
        private readonly IKorisnikService _korisnikService;

        public KorisnikController(ModelContext context, IKorisnikService korisnikService)
        {
            _context = context;
            _korisnikService = korisnikService;
        }

        // GET: api/Korisnik
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AUser>>> GetAUsers()
        {
            var retVal = await _korisnikService.GetAll();
            return retVal.ToList();
        }

        // GET: api/Korisnik/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AUser>> GetAUser(int id)
        {
            var aUser = await _korisnikService.Get(id);
            return aUser;
        }

        // POST: api/Korisnik
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AUser>> PostAUser(AUser aUser)
        {
            await _korisnikService.Add(aUser);
            return CreatedAtAction("GetAUser", new { id = aUser.Id }, aUser);
        }

        // PUT: api/Korisnik/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAUser(int id, AUser aUser)
        {
            await _korisnikService.Modify(id, aUser);
            return NoContent();
        }

        // DELETE: api/Korisnik/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAUser(int id)
        {
            await _korisnikService.Remove(id);
            return NoContent();
        }

        private bool AUserExists(int id)
        {
            return (_context.AUsers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
