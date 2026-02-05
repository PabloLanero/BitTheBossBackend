using BTB.Entities.Models;

namespace BTB.Service
{
    public interface IPartidaService
    {
        public Task<List<Partida>> GetPartidasAsync();
        public Task<Partida?> GetPartidaByIdAsync(string p_id);
        public Task<bool> AddPartidaAsync(Partida p_partida);
        public Task<bool> UpdatePartidaAsync(Partida p_partida);
        public Task<bool> DeletePartidaAsync(string p_id);
    }
}
