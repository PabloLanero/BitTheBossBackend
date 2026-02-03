using BTB.Entities.Models;
using BTB.Repository.Interfaces;

namespace BTB.Repository
{
    public class PartidaRepository : IPartidaRepository
    {
        private readonly List<Partida> _lst = new List<Partida>();

        public PartidaRepository()
        {
        }

        public Task<bool> DeletePartidaAsync(string id)
        {
            var existing = _lst.FirstOrDefault(p => p.IdPartida == id);
            if (existing == null) return Task.FromResult(false);
            _lst.Remove(existing);
            return Task.FromResult(true);
        }

        public Task<Partida?> GetPartidaByIdAsync(string id)
        {
            var partida = _lst.FirstOrDefault(p => p.IdPartida == id);
            return Task.FromResult(partida);
        }

        public Task<List<Partida>> GetPartidasAsync()
        {
            return Task.FromResult(_lst.ToList());
        }

        public Task<bool> PostPartidaAsync(Partida partida)
        {
            if (string.IsNullOrWhiteSpace(partida.IdPartida)) partida.IdPartida = Guid.NewGuid().ToString();
            _lst.Add(partida);
            return Task.FromResult(true);
        }

        public Task<bool> PutPartidaAsync(Partida partida)
        {
            var existing = _lst.FirstOrDefault(p => p.IdPartida == partida.IdPartida);
            if (existing == null) return Task.FromResult(false);
            existing.ArrUsuario = partida.ArrUsuario;
            existing.LstNodos = partida.LstNodos;
            return Task.FromResult(true);
        }
    }
}