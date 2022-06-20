using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.BL.Models;

namespace WebAPI.BL.Services
{
    public interface IKorisnikService
    {
        Task<AUser> Get(int id);
        Task<IEnumerable<AUser>> GetAll();
        Task Add(AUser aUser);
        Task Modify(int id, AUser aUser);
        Task Remove(int id);
    }

    public class KorisnikService : IKorisnikService
    {
        public readonly ModelContext _context;

        public KorisnikService(ModelContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AUser>> GetAll()
        {
            var retVal = await _context.AUsers.ToListAsync();
            return retVal;
        }

        public async Task<AUser> Get(int id)
        {
            var aUser = await _context.AUsers.FindAsync(id);
            return aUser;
        }

        public async Task Add(AUser aUser)
        {
            await _context.AUsers.AddAsync(aUser);

            await _context.SaveChangesAsync();
        }

        public async Task Modify(int id, AUser aUser)
        {
            _context.Entry(aUser).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        public async Task Remove(int id)
        {
            var aUser = await _context.AUsers.FindAsync(id);
            _context.AUsers.Remove(aUser);
            await _context.SaveChangesAsync();
        }
    }
}
