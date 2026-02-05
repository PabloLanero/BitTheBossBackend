using BTB.Entities.DTO;

namespace BTB.Service
{
    public interface ITierService
    {
        public Task<List<TierDTOOut>> GetTiersAsync();
        public Task<TierDTOOut?> GetTierByIdAsync(int id);
        public Task<TierDTOOut> AddTierAsync(TierDTOIn dto);
        public Task<bool> UpdateTierAsync(int id, TierDTOIn dto);
        public Task<bool> DeleteTierAsync(int id);
    }
}
