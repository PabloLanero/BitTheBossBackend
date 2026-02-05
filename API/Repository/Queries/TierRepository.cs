using BTB.Entities.Models;
using BTB.Repository.Interfaces;

namespace BTB.Repository
{
    public class TierRepository : ITierRepository
    {
        private readonly List<Tier> _lst = new List<Tier>();
        private int _id = 1;

        public TierRepository() {}

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
            return Task.FromResult(_lst.ToList());
        }

        public Task<bool> PostTierAsync(Tier tier)
        {
            tier.Id = _id++;
            _lst.Add(tier);
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
