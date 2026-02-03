using BTB.Entities.Models;

namespace BTB.Repository.Interfaces
{
    public interface IPartidaRepository
    {
        public Task<List<Partida>> GetPartidasAsync();
        public Task<Partida?> GetPartidaByIdAsync(string id);
        public Task<bool> PostPartidaAsync(Partida partida);
        public Task<bool> PutPartidaAsync(Partida partida);
        public Task<bool> DeletePartidaAsync(string id);
    }
}
