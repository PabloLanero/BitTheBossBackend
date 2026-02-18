using BTB.Entities.Models;

namespace BTB.Repository.Interfaces
{
    public interface ITierRepository
    {
        public Task<List<Tier>> GetTiersAsync();
        public Task<Tier?> GetTierByIdAsync(int id);
        public Task<Tier?> PostTierAsync(Tier tier);
        public Task<Tier?> PutTierAsync(Tier tier);
        public Task<bool> DeleteTierAsync(int id);
    }
}
