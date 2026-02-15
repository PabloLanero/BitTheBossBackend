using BTB.Data;
using BTB.Entities.Models;
using BTB.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BTB.Repository
{
    public class TierRepository : ITierRepository
    {
        private readonly List<Tier> _lst = new List<Tier>();
        private readonly BTBContext _context ;

        public TierRepository(BTBContext context)
        {
            _context = context;
        }

        public Task<bool> DeleteTierAsync(int id)
        {
            var existing = _lst.FirstOrDefault(t => t.Id == id);
            if (existing == null) return Task.FromResult(false);
            _lst.Remove(existing);
            return Task.FromResult(true);
        }

        public Task<Tier?> GetTierByIdAsync(int id)
        {
            var t = _lst.FirstOrDefault(x => x.Id == id);
            return Task.FromResult(t);
        }

        public Task<List<Tier>> GetTiersAsync()
        {
            var lista = _context.Tiers.AsQueryable<Tier>()
            .Include( t => t.LstUsuarios);
            
            return Task.FromResult(lista.ToList());
        }

        public Task<bool> PostTierAsync(Tier tier)
        {
            var newTier = _context.Tiers.Add(tier);
            _context.SaveChanges(); 
            return Task.FromResult(true);
        }

        public Task<bool> PutTierAsync(Tier tier)
        {
            var existing = _lst.FirstOrDefault(t => t.Id == tier.Id);
            if (existing == null) return Task.FromResult(false);
            existing.Titulo = tier.Titulo;
            existing.Visible = tier.Visible;
            return Task.FromResult(true);
        }
    }
}
