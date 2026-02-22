using BTB.Data;
using BTB.Entities.Models;
using BTB.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BTB.Repository
{
    public class TierRepository : ITierRepository
    {
        
        private readonly BTBContext _context ;

        public TierRepository(BTBContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteTierAsync(int id)
        {
            Tier tier = _context.Tiers.Remove(new Tier{Id=id}).Entity;
            return tier != null;
        }

        public async Task<Tier?> GetTierByIdAsync(int id)
        {
            Tier tier = await _context.Tiers.FindAsync(id) ?? new Tier();
            return tier;
        }

        public async Task<List<Tier>> GetTiersAsync()
        {
            var lista = _context.Tiers.AsQueryable<Tier>()
            .Include( t => t.UsuarioId );
            await _context.SaveChangesAsync(); 
            return lista.ToList();
        }

        public async Task<Tier?> PostTierAsync(Tier tier)
        {
            Tier newTier = _context.Tiers.Add(tier).Entity;
            await _context.SaveChangesAsync(); 
            return newTier;
        }

        public async Task<Tier?> PutTierAsync(Tier tier)
        {
            Tier newTier = _context.Update(tier).Entity;
            await _context.SaveChangesAsync();
            return newTier;
        }
    }
}
