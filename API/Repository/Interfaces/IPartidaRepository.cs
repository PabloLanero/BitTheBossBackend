using BTB.Entities.Models;

namespace BTB.Repository.Interfaces
{
    public interface IPartidaRepository
    {
        public List<Partida> GetPartidasAsync();
        public Task<Partida?> GetPartidaByIdAsync(string id);
        public Task<Partida> PostPartidaAsync(Partida partida);
        public Task<Partida> PutPartidaAsync(Partida partida);
        public Task<bool> DeletePartidaAsync(string id);
    }
}
