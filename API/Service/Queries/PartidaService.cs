using BTB.Entities.DTO;
using BTB.Entities.Models;
using BTB.Repository.Interfaces;
using BTB.Service.Common;

namespace BTB.Service
{
    public class PartidaService : IPartidaService
    {
        private readonly IPartidaRepository _repository;

        public PartidaService(IPartidaRepository repository)
        {
            _repository = repository;
        }

        public async Task<PartidaDTOOut> AddPartidaAsync(PartidaDTOIn dto)
        {
            if (dto == null) throw new ValidationException("PartidaDTOIn no puede ser null");
            if (dto.ArrUsuario == null || !dto.ArrUsuario.Any()) throw new ValidationException("La partida debe contener al menos un usuario.");
            if (dto.LstNodos != null)
            {
                var duplicate = dto.LstNodos.GroupBy(n => n.IdNodo).Any(g => g.Count() > 1);
                if (duplicate) throw new ValidationException("Hay nodos duplicados (IdNodo debe ser único).");
            }

            var model = MapDtoToModel(dto);
            await _repository.PostPartidaAsync(model);
            return MapModelToDto(model);
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

        public async Task<List<PartidaDTOOut>> GetPartidasAsync()
        {
            var lst = await _repository.GetPartidasAsync();
            return lst.Select(MapModelToDto).ToList();
        }

        public Task<bool> UpdatePartidaAsync(string id, PartidaDTOIn dto)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ValidationException("Id inválido para actualizar partida.");
            var existing = _repository.GetPartidaByIdAsync(id).GetAwaiter().GetResult();
            if (existing == null) throw new NotFoundException($"Partida con id {id} no encontrada.");

            var model = MapDtoToModel(dto);
            model.IdPartida = id;
            return _repository.PutPartidaAsync(model);
        }

        private Partida MapDtoToModel(PartidaDTOIn dto)
        {
            var partida = new Partida
            {
                IdPartida = dto.IdPartida ?? Guid.NewGuid().ToString(),
                ArrUsuario = dto.ArrUsuario?.Select(u => new Usuario { Id = u.Id ?? 0, Nombre = u.Nombre ?? string.Empty, Correo = u.Correo ?? string.Empty }).ToArray() ?? new Usuario[0],
                LstNodos = dto.LstNodos?.Select(n => new Nodo { IdNodo = (byte)n.IdNodo, ArrTropas = (n.ArrTropas ?? new List<TropaDTOIn>()).Select(t => new Tropa { Nombre = t.Nombre ?? string.Empty, Vida = t.Vida, Damage = t.Damage }).ToArray(), DuenoNodo = n.DuenoNodo == null ? null : new Usuario { Id = n.DuenoNodo.Id ?? 0, Nombre = n.DuenoNodo.Nombre ?? string.Empty, Correo = n.DuenoNodo.Correo ?? string.Empty } }).ToList() ?? new List<Nodo>()
            };

            return partida;
        }

        private PartidaDTOOut MapModelToDto(Partida partida)
        {
            if (partida == null) return new PartidaDTOOut();

            return new PartidaDTOOut
            {
                IdPartida = partida.IdPartida,
                ArrUsuario = partida.ArrUsuario?.Select(u => new UsuarioRefDTOOut { Id = u.Id, Nombre = u.Nombre, Correo = u.Correo }).ToList() ?? new List<UsuarioRefDTOOut>(),
                LstNodos = partida.LstNodos?.Select(n => new NodoDTOOut {
                    IdNodo = n.IdNodo,
                    ArrTropas = n.ArrTropas?.Select(t => new TropaDTOOut { Id = t.Id, Nombre = t.Nombre, Vida = t.Vida, Damage = t.Damage }).ToList() ?? new List<TropaDTOOut>(),
                    DuenoNodo = n.DuenoNodo == null ? null : new UsuarioRefDTOOut { Id = n.DuenoNodo.Id, Nombre = n.DuenoNodo.Nombre, Correo = n.DuenoNodo.Correo }
                }).ToList() ?? new List<NodoDTOOut>()
            };
        }
    }
}
