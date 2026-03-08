using System.Threading.Tasks;
using BTB.Entities.DTO;
using BTB.Entities.Models;
using BTB.Repository.Interfaces;
using BTB.Service.Common;

namespace BTB.Service
{
    public class PartidaService : IPartidaService
    {
        private readonly IPartidaRepository _repository;

        public PartidaService(IPartidaRepository repository, INodoService nodoService, IUsuarioService usuarioService)
        {
            _repository = repository;
            _ = nodoService;
            _ = usuarioService;
        }

        public async Task<PartidaDTOOut> AddPartidaAsync(PartidaDTOIn dto)
        {
            if (dto == null) throw new ValidationException("PartidaDTOIn no puede ser null");

            var requestedId = (dto.IdPartida ?? string.Empty).Trim();
            if (string.IsNullOrWhiteSpace(requestedId))
                throw new ValidationException("El nombre de la partida es obligatorio.");

            var finalId = await EnsureUniquePartidaIdAsync(requestedId);
            var model = new Partida
            {
                IdPartida = finalId,
                LstNodos = new List<Nodo>()
            };

            var created = await _repository.PostPartidaAsync(model);
            return MapModelToDto(created);
        }

        public Task<bool> DeletePartidaAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return Task.FromResult(false);
            return _repository.DeletePartidaAsync(id);
        }

        public async Task<PartidaDTOOut?> GetPartidaByIdAsync(string id)
        {
            var p = await _repository.GetPartidaByIdAsync(id);
            if (p == null) return null;
            return MapModelToDto(p);
        }

        public Task<List<PartidaDTOOut>> GetPartidasAsync()
        {
            var lst = _repository.GetPartidasAsync();
            var mapped = lst.Select(MapModelToDto).ToList();
            return Task.FromResult(mapped);
        }

        public async Task<PartidaDTOOut> UpdatePartidaAsync(string id, PartidaDTOIn dto)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ValidationException("Id invalido para actualizar partida.");

            if (dto == null)
                throw new ValidationException("PartidaDTOIn no puede ser null");

            var existing = await _repository.GetPartidaByIdAsync(id);
            if (existing == null)
                throw new NotFoundException($"Partida con id {id} no encontrada.");

            var partida = await _repository.PutPartidaAsync(existing);
            return MapModelToDto(partida);
        }

        private async Task<string> EnsureUniquePartidaIdAsync(string baseId)
        {
            var candidate = baseId;
            var suffix = 1;

            while (await _repository.GetPartidaByIdAsync(candidate) != null)
            {
                candidate = $"{baseId}-{suffix}";
                suffix++;
            }

            return candidate;
        }

        private static PartidaDTOOut MapModelToDto(Partida partida)
        {
            if (partida == null) return new PartidaDTOOut();

            return new PartidaDTOOut
            {
                IdPartida = partida.IdPartida,
                ArrUsuario = new List<UsuarioRefDTOOut>(),
                LstNodos = new List<NodoDTOOut>()
            };
        }
    }
}
