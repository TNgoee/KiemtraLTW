using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Kiemtra.Models
{
    public class EFBadmintonRepository : IBadmintonRepository
    {
        private readonly BadmintonDbContext _context;
        public EFBadmintonRepository(BadmintonDbContext context)
        {
            _context = context;
        }

        // GiaiDau
        public async Task<IEnumerable<GiaiDau>> GetAllGiaiDauAsync()
        {
            return await _context.GiaiDaus.ToListAsync();
        }

        public async Task<GiaiDau> GetGiaiDauByIdAsync(string id)
        {
            return await _context.GiaiDaus.FindAsync(id);
        }

        public async Task AddGiaiDauAsync(GiaiDau entity)
        {
            _context.GiaiDaus.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateGiaiDauAsync(GiaiDau entity)
        {
            _context.GiaiDaus.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteGiaiDauAsync(string id)
        {
            var entity = await _context.GiaiDaus.FindAsync(id);
            if (entity != null)
            {
                _context.GiaiDaus.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}