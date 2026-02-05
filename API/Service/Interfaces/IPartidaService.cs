using BTB.Entities.DTO;

namespace BTB.Service
{
    public interface IPartidaService
    {
        public Task<List<PartidaDTOOut>> GetPartidasAsync();
        public Task<PartidaDTOOut?> GetPartidaByIdAsync(string id);
        public Task<PartidaDTOOut> AddPartidaAsync(PartidaDTOIn dto);
        public Task<bool> UpdatePartidaAsync(string id, PartidaDTOIn dto);
        public Task<bool> DeletePartidaAsync(string id);
    }
}
