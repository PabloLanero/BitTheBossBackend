using BTB.Entities.Models;
using BTB.Repository.Interfaces;

namespace BTB.Service
{
    public class PartidaService : IPartidaService
    {
        private readonly IPartidaRepository _repository;

        public PartidaService(IPartidaRepository repository)
        {
            _repository = repository;
        }

        public Task<bool> AddPartidaAsync(Partida p_partida)
        {
            if (p_partida == null) throw new ArgumentNullException(nameof(p_partida));
            return _repository.PostPartidaAsync(p_partida);
        }

        public Task<bool> DeletePartidaAsync(string p_id)
        {
            if (string.IsNullOrWhiteSpace(p_id)) return Task.FromResult(false);
            return _repository.DeletePartidaAsync(p_id);
        }

        public Task<Partida?> GetPartidaByIdAsync(string p_id)
        {
            if (string.IsNullOrWhiteSpace(p_id)) return Task.FromResult<Partida?>(null);
            return _repository.GetPartidaByIdAsync(p_id);
        }

        public Task<List<Partida>> GetPartidasAsync()
        {
            return _repository.GetPartidasAsync();
        }

        public Task<bool> UpdatePartidaAsync(Partida p_partida)
        {
            if (p_partida == null) throw new ArgumentNullException(nameof(p_partida));
            return _repository.PutPartidaAsync(p_partida);
        }
    }
}
