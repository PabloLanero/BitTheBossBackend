using BTB.Entities.DTO;

namespace BTB.Service
{
    public interface INodoService
    {
        public Task<List<NodoDTOOut>> GetNodosAsync();
        public Task<NodoDTOOut?> GetNodoByIdAsync(int id);
        public Task<NodoDTOOut> AddNodoAsync(NodoDTOIn dto);
        public Task<bool> UpdateNodoAsync(int id, NodoDTOIn dto);
        public Task<bool> DeleteNodoAsync(int id);
    }
}
